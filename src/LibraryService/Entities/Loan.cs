using System.ComponentModel.DataAnnotations;

namespace LibraryService.Entities;

public class Loan
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public Guid BookId { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public Guid EmployeeId { get; set; }
    public string Observation { get; set; }
    [Required]
    public DateTime LoanDate { get; set; }
    [Required]
    public DateTime ReturnDate { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }
}
