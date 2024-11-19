using System;
using AutoMapper;
using Contracts;
using MassTransit;
using Microsoft.VisualBasic;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers;

public class BookCreatedConsumer : IConsumer<BookCreated>
{
    private readonly IMapper _mapper;

    public BookCreatedConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public async Task Consume(ConsumeContext<BookCreated> context)
    {
        Console.WriteLine($"BookCreatedConsumer: {context.Message.Id}");

        var book = _mapper.Map<Book>(context.Message);

        await book.SaveAsync();
    }
}
