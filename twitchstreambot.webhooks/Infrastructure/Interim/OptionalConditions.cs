using System.Text.Json.Serialization;

namespace twitchstreambot.webhooks.Infrastructure.Interim;

public class OptionalConditions : SubscriptionCondition
{
    [JsonPropertyName("broadcaster_user_id")]
    public string? BroadcasterUserId { get; set; }
    
    [JsonPropertyName("user_id")]
    public string? UserId { get; set; }
}