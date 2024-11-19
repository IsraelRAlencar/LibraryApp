using System.ComponentModel.DataAnnotations;
using LibraryService.DTOs.BookDTOs;
using LibraryService.DTOs.UserDTOs;

namespace LibraryService.DTOs.LoanDTOs;

public class CreateLoanDto
{
    [Required]
    public BookDto Book { get; set; }
    [Required]
    public UserDto User { get; set; }
    public string Observation { get; set; }
    [Required]
    public DateTime LoanDate { get; set; }
    [Required]
    public DateTime ReturnDate { get; set; }
}
