using System.Text;
using System.Text.Json;
using twitchstreambot.Api;
using twitchstreambot.Api.Requests;
using twitchstreambot.Api.Requests.WebHook;
using twitchstreambot.Infrastructure;

namespace twitchstreambot.webhooks.Api;

public class SubscribeWebhookRequest(TwitchHelix api)
    : HelixRequest<SubscribeWebhookRequest, HelixWebHookCollectionResponse<WebhookSubscriptionContent>>(api)
{
    public override async Task<HelixWebHookCollectionResponse<WebhookSubscriptionContent>?> Execute(
        SubscribeWebhookRequest parameters)
    {
        var response = await api.Client.PostAsync("helix/eventsub/subscriptions",
            new StringContent(JsonSerializer.Serialize(parameters), Encoding.UTF8, "application/json"));

        if (!response.IsSuccessStatusCode) return null;

        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<HelixWebHookCollectionResponse<WebhookSubscriptionContent>>(content);
    }
}