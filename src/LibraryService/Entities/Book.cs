using System.ComponentModel.DataAnnotations;

namespace LibraryService.Entities;

public class Book
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Author { get; set; }
    public string Publisher { get; set; }
    public int Year { get; set; }
    public int Pages { get; set; }
    public string Isbn { get; set; }
    public List<Category> Categories { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }
}
