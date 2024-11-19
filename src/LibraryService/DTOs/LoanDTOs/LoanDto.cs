using LibraryService.Entities;

namespace LibraryService.DTOs.LoanDTOs;

public class LoanDto
{
    public Guid Id { get; set; }
    public Book Book { get; set; }
    public User User { get; set; }
    public string Observation { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }
}
