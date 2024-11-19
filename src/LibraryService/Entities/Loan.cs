using System.ComponentModel.DataAnnotations;

namespace LibraryService.Entities;

public class Loan
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public Book Book { get; set; }
    [Required]
    public User User { get; set; }
    public string Observation { get; set; }
    [Required]
    public DateTime LoanDate { get; set; }
    [Required]
    public DateTime ReturnDate { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }
}
