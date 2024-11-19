using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryService.Data.Interfaces;
using LibraryService.DTOs.BookDTOs;
using LibraryService.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Data.Repositories;

public class BookRepository : IBookRepository
{
    private readonly LibraryDbContext _context;
    private readonly IMapper _mapper;

    public BookRepository(LibraryDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void AddBook(Book book)
    {
        _context.Books.Add(book);
    }

    public async Task<BookDto> GetBookByIdAsync(Guid id)
    {
        return await _context.Books.ProjectTo<BookDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Book> GetBookEntityByIdAsync(Guid id)
    {
        return await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<List<BookDto>> GetBooksAsync()
    {
        return await _context.Books.ProjectTo<BookDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public void RemoveBook(Book book)
    {
        _context.Books.Remove(book);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
