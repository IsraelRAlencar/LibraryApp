using System.ComponentModel.DataAnnotations;

namespace LibraryService.Entities;

public class Image
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public Book Book { get; set; }
    [Required]
    public string ImageUrl { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
