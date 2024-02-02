using System.Text.Json.Serialization;

namespace twitchstreambot.webhooks.Infrastructure.Interim;

public class BroadcasterOnlyCondition : SubscriptionCondition
{
    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; set; } = null!;
}