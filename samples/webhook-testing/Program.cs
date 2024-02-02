using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using twitchstreambot.Api;
using twitchstreambot.Infrastructure.Extensions;
using twitchstreambot.webhooks.Api;
using twitchstreambot.webhooks.Api.Parameters;
using twitchstreambot.webhooks.Events.Channel;
using twitchstreambot.webhooks.Extensions;
using twitchstreambot.webhooks.Infrastructure.Interim;
using twitchstreambot.webhooks.Publishing;
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

    configure.AddLocalEventHandling(builder => { builder.AddHandlersFromAssembly(typeof(CheerHandler).Assembly); });
});

builder.Services.AddHelix(builder.Configuration);
builder.Services.AddScoped<SubscribeWebhookRequest<Follow>>();

var app = builder.Build();

app.MapGet("/verify", async ([FromServices] TwitchHelix helix) =>
{
    
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseWebhooks();

app.Run();