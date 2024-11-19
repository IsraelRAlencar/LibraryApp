using System;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers;

public class BookDeletedConsumer : IConsumer<BookDeleted>
{
    public async Task Consume(ConsumeContext<BookDeleted> context)
    {
        Console.WriteLine($"BookDeletedConsumer: {context.Message.Id}");

        var result = await DB.DeleteAsync<Book>(context.Message.Id);

        if (!result.IsAcknowledged)
            throw new MessageException(typeof(BookDeleted), "Failed to delete book.");
    }
}
