using System.Text;
using System.Text.Json;
using twitchstreambot.Api;
using twitchstreambot.Api.Requests;
using twitchstreambot.Infrastructure;
using twitchstreambot.webhooks.Api.Parameters;
using twitchstreambot.webhooks.Api.Response;
using twitchstreambot.webhooks.Events;
using twitchstreambot.webhooks.Infrastructure.Delegates;

namespace twitchstreambot.webhooks.Api;

public class SubscribeWebhookRequest<TSubscriptionEvent>(TwitchHelix api, CreateTwitchWebhookOptionsDelegate options)
    : HelixRequest<WebhookSubscriptionParameters<TSubscriptionEvent>,
        HelixWebHookCollectionResponse<WebhookSubscriptionContent>>(api)
    where TSubscriptionEvent : WebhookBaseEvent
{
    private readonly TwitchHelix _api = api;

    public override async Task<HelixWebHookCollectionResponse<WebhookSubscriptionContent>?> Execute(
        WebhookSubscriptionParameters<TSubscriptionEvent> parameters)
    {
        var content =
            new StringContent(JsonSerializer.Serialize(parameters, options()), Encoding.UTF8, "application/json");

        var client = _api.Client;

        Configure(client);

        var response = await client.PostAsync("helix/eventsub/subscriptions", content);

        if (!response.IsSuccessStatusCode) return null;

        var result = await response.Content.ReadAsStringAsync();

        return JsonSerializer
            .Deserialize<HelixWebHookCollectionResponse<WebhookSubscriptionContent>>(result, options());
    }
}