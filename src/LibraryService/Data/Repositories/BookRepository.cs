using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contracts;
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

    public async Task<List<BookDto>> GetBooksAsync(string date)
    {
        var query = _context.Books.OrderBy(x => x.Title).AsQueryable();

        if (!string.IsNullOrEmpty(date))
        {
            query = query.Where(x => x.UpdatedAt.CompareTo(DateTime.Parse(date).ToUniversalTime()) > 0);
        }

        return await query.ProjectTo<BookDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<List<BookCreated>> GetBooksAsyncSearchDb(string date)
    {
        var query = _context.Books.OrderBy(x => x.Title).AsQueryable();

        if (!string.IsNullOrEmpty(date))
        {
            query = query.Where(x => x.UpdatedAt.CompareTo(DateTime.Parse(date).ToUniversalTime()) > 0);
        }

        var books = await query.ProjectTo<BookDto>(_mapper.ConfigurationProvider).ToListAsync();

        return _mapper.Map<List<BookCreated>>(books);
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