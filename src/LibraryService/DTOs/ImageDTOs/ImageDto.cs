using LibraryService.Entities;

namespace LibraryService.DTOs.ImageDTOs;

public class ImageDto
{
    public Guid Id { get; set; }
    public Book Book { get; set; }
    public string ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
}
