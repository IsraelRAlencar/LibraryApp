using System.ComponentModel.DataAnnotations;

namespace LibraryService.Entities;

public class Category
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Book> Books { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }
}
