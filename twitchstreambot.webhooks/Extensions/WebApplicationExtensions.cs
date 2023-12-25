﻿using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using twitchstreambot.webhooks.Extensions.Builders;
using twitchstreambot.webhooks.Infrastructure;
using twitchstreambot.webhooks.Infrastructure.Contracts;
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
            [FromServices] IWebhookEventDispatcher dispatcher) =>
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

            await dispatcher.Dispatch(message, content);

            return TypedResults.Ok();
        });

        return app;
    }

    public static async Task<string> GetContentAsString(Stream content)
    {
        string body = string.Empty;

        using var reader = new StreamReader(content);

        body = await reader.ReadToEndAsync();

        return body;
    }
}