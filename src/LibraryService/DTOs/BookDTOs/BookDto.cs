using LibraryService.Entities;

namespace LibraryService.DTOs.BookDTOs;

public class BookDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Publisher { get; set; }
    public int Year { get; set; }
    public int Pages { get; set; }
    public string Isbn { get; set; }
    public List<Category> Categories { get; set; }
    public List<Image> Images { get; set; }
    public List<Loan> Loans { get; set; }
    public List<Reservation> Reservations { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }
}
