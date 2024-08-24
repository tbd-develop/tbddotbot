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

builder.Services.AddTwitch(configure =>
{
    configure
        .AddIrcBot(configure => { });
});

//
// builder.Services.AddTwitchStreamBot(configure =>
//     configure.AddCommands(typeof(Program).Assembly));
//
// builder.Services.AddWebhooks(configure =>
// {
//     configure.AddSecretProvider<SampleSecretProvider>((provider, headers, _) =>
//         provider.SecretForSubscriptionType(headers.SubscriptionType!));
//
//     configure.AddLocalEventHandling(builder => { builder.AddHandlersFromAssembly(typeof(CheerHandler).Assembly); });
// });
//
// builder.Services.AddTwitchApi(builder.Configuration);
// builder.Services.AddScoped<SubscribeWebhookRequest<Follow>>();

var app = builder.Build();

app.MapGet("/verify", async (
    [FromServices] IConfiguration configuration,
    [FromServices] TwitchApi api,
    [FromServices] TwitchHelix helix) =>
{
    var response = await helix.GetUsersByName("tbdgamer");

    if (response?.HasData ?? false)
    {
        var broadcaster = response.Data.First();

        var authentication =
            await api.AuthorizeClientCredentials();

        var subscribeRequest = helix.GetRequest<SubscribeWebhookRequest<Follow>>();

        if (authentication is not null && subscribeRequest is not null)
        {
            subscribeRequest.OnConfiguring += delegate(HttpClient client)
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", authentication.AccessToken);
            };

            await subscribeRequest
                .Execute(new WebhookSubscriptionParameters<Follow>(
                    new BroadcasterOnlyCondition
                    {
                        BroadcasterUserId = $"{broadcaster.Id}"
                    }, new SubscriptionTransportDefinition
                    {
                        Callback = "https://tbddotbot.ngrok.io/api/eventsub",
                        Method = "webhook",
                        Secret = "this-is-a-secret"
                    }));
        }
    }
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