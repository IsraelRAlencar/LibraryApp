using System.ComponentModel.DataAnnotations;
using LibraryService.Entities;

namespace LibraryService.DTOs.CategoryDTOs;

public class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Book> Books { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
