using Polly;
using Polly.Extensions.Http;
using MassTransit;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// builder.Services.AddHttpClient<LibrarySvcHttpClient>().AddPolicyHandler(GetPolicy());
builder.Services.AddMassTransit(x =>
{
    // x.AddConsumersFromNamespaceContaining<BookCreatedConsumer>();
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("search-service", false));

    x.UsingRabbitMq((context, cfg) => {
        cfg.Host(builder.Configuration["RabbitMq:Host"], "/", host => {
            host.Username(builder.Configuration.GetValue("RabbitMq:Username", "guest"));
            host.Password(builder.Configuration.GetValue("RabbitMq:Password", "guest"));
        });

        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();

// app.Lifetime.ApplicationStarted.Register(async () => 
// {
//     await Policy.Handle<TimeoutException>()
//         .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(5))
//         .ExecuteAndCaptureAsync(async () => await DbInitializer.InitDb(app));
// });

app.Run();

// static IAsyncPolicy<HttpResponseMessage> GetPolicy()
//     => HttpPolicyExtensions
//         .HandleTransientHttpError()
//         .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
//         .WaitAndRetryForeverAsync(_ => TimeSpan.FromSeconds(3));