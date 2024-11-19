using System.ComponentModel.DataAnnotations;
using LibraryService.DTOs.BookDTOs;

namespace LibraryService.DTOs.CategoryDTOs;

public class CreateCategoryDto
{
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    public List<BookDto> Books { get; set; }
}
