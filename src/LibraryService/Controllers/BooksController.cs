using AutoMapper;
using Contracts;
using LibraryService.Data.Interfaces;
using LibraryService.DTOs.BookDTOs;
using LibraryService.Entities;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace LibraryService.Controllers
{
    [ApiController]
    [Route("library/books")]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _repo;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public BooksController(IBookRepository repo, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _repo = repo;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookDto>>> GetAllBooks(string date)
        {
            return await _repo.GetBooksAsync(date);
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<BookCreated>>> GetAllBooksSearchDb(string date)
        {
            return await _repo.GetBooksAsyncSearchDb(date);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBookById(Guid id)
        {
            var book = await _repo.GetBookByIdAsync(id);
            
            if (book == null) return NotFound();

            return book;
        }

        [HttpPost]
        public async Task<ActionResult<BookDto>> CreateBook(CreateBookDto createBookDto)
        {
            var book = _mapper.Map<Book>(createBookDto);
            
            _repo.AddBook(book);

            var newBook = _mapper.Map<BookDto>(book);

            await _publishEndpoint.Publish(_mapper.Map<BookCreated>(newBook));

            var result = await _repo.SaveChangesAsync();

            if (!result) return BadRequest("Error while creating book");

            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, _mapper.Map<BookDto>(book));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BookDto>> UpdateBook(Guid id, UpdateBookDto updateBookDto)
        {
            var book = await _repo.GetBookByIdAsync(id);

            if (book == null) return NotFound();

            book.Title = updateBookDto.Title ?? book.Title;
            book.Author = updateBookDto.Author ?? book.Author;
            book.Publisher = updateBookDto.Publisher ?? book.Publisher;
            book.Year = updateBookDto.Year != 0 ? updateBookDto.Year : book.Year;
            book.Pages = updateBookDto.Pages != 0 ? updateBookDto.Pages : book.Pages;
            book.Isbn = updateBookDto.Isbn ?? book.Isbn;
            book.Images.AddRange(updateBookDto.Images);
            book.Categories.AddRange(updateBookDto.Categories);

            await _publishEndpoint.Publish(_mapper.Map<BookUpdated>(book));

            var result = await _repo.SaveChangesAsync();

            if (!result) return BadRequest("Error while updating book");

            return _mapper.Map<BookDto>(book);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(Guid id)
        {
            var book = await _repo.GetBookEntityByIdAsync(id);

            if (book == null) return NotFound();

            _repo.RemoveBook(book);

            await _publishEndpoint.Publish(_mapper.Map<BookDeleted>(book));

            var result = await _repo.SaveChangesAsync();

            if (!result) return BadRequest("Error while deleting book");

            return NoContent();
        }
    }
}
