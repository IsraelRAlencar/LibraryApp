using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers;

public class BookUpdatedConsumer : IConsumer<BookUpdated>
{
    private readonly IMapper _mapper;

    public BookUpdatedConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<BookUpdated> context)
    {
        Console.WriteLine($"BookUpdatedConsumer: {context.Message.Id}");

        var book = _mapper.Map<Book>(context.Message);

        var result = await DB.Update<Book>()
            .Match(b => b.ID == book.ID)
            .ModifyOnly(b => new {
                b.Title,
                b.Author,
                b.Publisher,
                b.Isbn,
                b.Pages,
                b.Year
            }, book).ExecuteAsync();

        if (!result.IsAcknowledged)
            throw new MessageException(typeof(BookUpdated), "Failed to update book.");
    }
}
