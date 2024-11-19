using System.ComponentModel.DataAnnotations;

namespace LibraryService.Entities;

public class Reservation
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public Book Book { get; set; }
    [Required]
    public User User { get; set; }
    [Required]
    public string Observation { get; set; }
    public DateTime ReservationDate { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
