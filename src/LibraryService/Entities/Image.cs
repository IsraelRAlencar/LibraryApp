namespace LibraryService.Entities;

public class Image
{
    public Guid Id { get; set; }
    public Book Book { get; set; }
    public string ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
}
