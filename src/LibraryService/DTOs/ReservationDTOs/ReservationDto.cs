using LibraryService.Entities;

namespace LibraryService.DTOs.ReservationDTOs;

public class ReservationDto
{
    public Guid Id { get; set; }
    public Book Book { get; set; }
    public User User { get; set; }
    public string Observation { get; set; }
    public DateTime ReservationDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
