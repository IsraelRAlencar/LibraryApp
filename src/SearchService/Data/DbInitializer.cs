using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Models;
using SearchService.Services;

namespace SearchService.Data;

public class DbInitializer
{
    public static async Task InitDb(WebApplication app)
    {
        await DB.InitAsync("SearchDb", MongoClientSettings.
            FromConnectionString(
                app.Configuration.GetConnectionString("MongoDbConnection")
            )
        );

        await DB.Index<Book>()
            .Key(b => b.Title, KeyType.Text)
            .Key(b => b.Author, KeyType.Text)
            .Key(b => b.Publisher, KeyType.Text)
            .Key(b => b.Categories, KeyType.Text)
            .CreateAsync();

        var count = await DB.CountAsync<Book>();

        using var scope = app.Services.CreateScope();

        var httpClient = scope.ServiceProvider.GetRequiredService<LibrarySvcHttpClient>();
        var books = await httpClient.GetBooksForSearchDb();

        Console.WriteLine(books.Count + " books fetched from LibraryService.");
        if (books.Count > 0) await DB.SaveAsync(books);
    }
}
