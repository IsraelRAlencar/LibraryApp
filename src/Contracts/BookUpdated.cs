namespace Contracts;

public class BookUpdated
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Publisher { get; set; }
    public int Year { get; set; }
    public int Pages { get; set; }
    public string Isbn { get; set; }
    public List<string> Categories { get; set; }
    public List<string> Images { get; set; }
}
