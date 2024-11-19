using System.ComponentModel.DataAnnotations;
using LibraryService.DTOs.CategoryDTOs;
using LibraryService.DTOs.ImageDTOs;

namespace LibraryService.DTOs.BookDTOs;

public class CreateBookDto
{
    [Required]
    public string Title { get; set; }
    [Required]
    public string Author { get; set; }
    public string Publisher { get; set; }
    public int Year { get; set; }
    public int Pages { get; set; }
    public string Isbn { get; set; }
    public List<CategoryDto> Categories { get; set; }
    public List<ImageDto> Images { get; set; }
}
