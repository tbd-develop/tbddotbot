using System.Text.Json;
using twitchstreambot.Api;
using twitchstreambot.Api.Requests;
using twitchstreambot.Infrastructure;
using twitchstreambot.webhooks.Api.Response;
using twitchstreambot.webhooks.Infrastructure.Delegates;
using twitchstreambot.webhooks.Infrastructure.Interim;

namespace twitchstreambot.webhooks.Api;

public class GetSubscribedWebhooksRequest(TwitchHelix api, CreateTwitchWebhookOptionsDelegate options)
    : HelixRequest<HelixWebHookCollectionResponse<
        WebhookSubscriptionContent<OptionalConditions, SubscriptionTransportDefinition>>>(api)
{
    private readonly TwitchHelix _api = api;

    public override async Task<HelixWebHookCollectionResponse<
        WebhookSubscriptionContent<OptionalConditions, SubscriptionTransportDefinition>>?> Execute()
    {
        var response = await _api.Client.GetAsync("helix/eventsub/subscriptions");

        if (!response.IsSuccessStatusCode) return null;

        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer
            .Deserialize<HelixWebHookCollectionResponse<
                WebhookSubscriptionContent<OptionalConditions, SubscriptionTransportDefinition>>>(content, options());
    }
}