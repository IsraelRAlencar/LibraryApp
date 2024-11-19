using LibraryService.DTOs.BookDTOs;
using LibraryService.Entities;

namespace LibraryService.Data.Interfaces;

public interface IBookRepository
{
    Task<List<BookDto>> GetBooksAsync();
    Task<BookDto> GetBookByIdAsync(Guid id);
    Task<Book> GetBookEntityByIdAsync(Guid id);
    void AddBook(Book book);
    void RemoveBook(Book book);
    Task<bool> SaveChangesAsync();
}
