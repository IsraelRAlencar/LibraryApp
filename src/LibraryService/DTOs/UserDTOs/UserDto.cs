using LibraryService.Entities;

namespace LibraryService.DTOs.UserDTOs;

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<Loan> Loans { get; set; }
    public List<Reservation> Reservations { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
