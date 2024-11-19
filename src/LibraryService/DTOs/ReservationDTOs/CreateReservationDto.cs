using System.ComponentModel.DataAnnotations;
using LibraryService.DTOs.BookDTOs;
using LibraryService.DTOs.UserDTOs;

namespace LibraryService.DTOs.ReservationDTOs;

public class CreateReservationDto
{
    [Required]
    public BookDto Book { get; set; }
    [Required]
    public UserDto User { get; set; }
    [Required]
    public string Observation { get; set; }
    public DateTime ReservationDate { get; set; }
}
