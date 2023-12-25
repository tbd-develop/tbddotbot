using System.Text.Json.Serialization;
using twitchstreambot.Infrastructure;
using twitchstreambot.webhooks.Events.Values;
using twitchstreambot.webhooks.Infrastructure;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel.Subscription;

[WebhookEvent("channel.subscription.message")]
public class Message : Subscribe
{
    [JsonPropertyName("message")] public SubscribeMessage Content { get; set; } = null!;

    [JsonPropertyName("cumulative_months")]
    public int CumulativeMonths { get; set; }

    [JsonPropertyName("streak_months")] public int? StreakMonths { get; set; }

    [JsonPropertyName("duration_months")] public int DurationMonths { get; set; }
}