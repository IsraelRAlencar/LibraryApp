using System.ComponentModel.DataAnnotations;
using LibraryService.DTOs.BookDTOs;

namespace LibraryService.DTOs.ImageDTOs;

public class CreateImageDto
{
    [Required]
    public BookDto Book { get; set; }
    [Required]
    public string ImageUrl { get; set; }
}
