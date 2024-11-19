using System.ComponentModel.DataAnnotations;
using LibraryService.Entities;

namespace LibraryService.DTOs.BookDTOs;

public class UpdateBookDto
{
    [Required]
    public string Title { get; set; }
    [Required]
    public string Author { get; set; }
    public string Publisher { get; set; }
    public int Year { get; set; }
    public int Pages { get; set; }
    public string Isbn { get; set; }
    public List<Category> Categories { get; set; }
    public List<Image> Images { get; set; }
}
