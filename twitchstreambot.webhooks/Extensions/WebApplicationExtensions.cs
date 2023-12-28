using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using twitchstreambot.webhooks.Extensions.Builders;
using twitchstreambot.webhooks.Infrastructure;
using twitchstreambot.webhooks.Models;

namespace twitchstreambot.webhooks.Extensions;

public static class WebApplicationExtensions
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public static WebApplication UseWebhooks(this WebApplication app,
        string uri = "/api/eventsub")
    {
        app.MapPost(uri, async (HttpRequest request,
            [FromServices] GetSecretDelegate secretProvider,
            [FromServices] EventMapper eventMapper,
            [FromServices] IEventPublisher eventPublisher) =>
        {
            var twitchHeaders = TwitchHeaderCollection.FromHeaders(request.Headers);

            if (!twitchHeaders.IsValid)
            {
                return Results.Forbid();
            }

            var content = await GetContentAsString(request.Body);

            var secretForMessageType = secretProvider(twitchHeaders, request);

            var validator = new MessageValidator(secretForMessageType);

            if (!validator.IsValid(
                    twitchHeaders.MessageId!,
                    twitchHeaders.MessageTimestampString!,
                    content,
                    twitchHeaders.MessageSignature!
                ))
            {
                return TypedResults.Forbid();
            }
            
            var message = JsonSerializer.Deserialize<IncomingSubscriptionMessage>(content, SerializerOptions);

            if (message is null)
            {
                return TypedResults.BadRequest();
            }

            var @event = eventMapper.Map(message, content);

            if (@event is null)
            {
                return TypedResults.BadRequest();
            }

            await eventPublisher.Publish(@event, twitchHeaders);

            return TypedResults.Ok();
        });

        return app;
    }

    private static async Task<string> GetContentAsString(Stream content)
    {
        using var reader = new StreamReader(content);

        return await reader.ReadToEndAsync();
    }
}