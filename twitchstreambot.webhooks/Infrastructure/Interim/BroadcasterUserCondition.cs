using System.Text.Json.Serialization;

namespace twitchstreambot.webhooks.Infrastructure.Interim;

public class BroadcasterUserCondition : SubscriptionCondition
{
    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; set; } = null!;

    [JsonPropertyName("user_id")] public string UserId { get; set; } = null!;
}