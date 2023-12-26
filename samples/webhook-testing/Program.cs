using twitchstreambot.webhooks.Extensions;
using twitchstreambot.webhooks.Infrastructure;
using webhook_testing.Handlers;
using webhook_testing.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddWebhooks(configure =>
{
    configure.AddSecretProvider<SampleSecretProvider>((provider, headers, _) =>
        provider.SecretForSubscriptionType(headers.SubscriptionType!));

    configure.AddLocalEventHandling(builder =>
    {
        builder.AddHandlersFromAssembly(typeof(CheerHandler).Assembly);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseWebhooks();

app.Run();