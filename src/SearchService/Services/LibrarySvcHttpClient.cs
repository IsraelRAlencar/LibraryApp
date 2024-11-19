using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Services;

public class LibrarySvcHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public LibrarySvcHttpClient(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task<List<Book>> GetBooksForSearchDb()
    {
        var lastUpdated = await DB.Find<Book, string>()
            .Sort(x => x.Descending(b => b.UpdatedAt))
            .Project(b => b.UpdatedAt.ToString())
            .ExecuteFirstAsync();

        return await _httpClient.GetFromJsonAsync<List<Book>>($"{_config["LibraryServiceUrl"]}/library/books/search?lastUpdated={lastUpdated}");
    }
}
