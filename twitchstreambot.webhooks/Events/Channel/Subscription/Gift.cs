using System.Text.Json.Serialization;
using twitchstreambot.Infrastructure;
using twitchstreambot.webhooks.Infrastructure;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel.Subscription;

[WebhookEvent("channel.subscription.gift")]
public class Gift : Subscribe
{
    public int Total { get; set; }
    [JsonPropertyName("is_anonymous")] public bool IsAnonymous { get; set; }
    [JsonPropertyName("cumulative_total")] public int CumulativeTotal { get; set; }
}