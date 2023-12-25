using System.Text.Json.Serialization;

namespace twitchstreambot.Api.Requests.WebHook;

public class BroadcasterOnlyCondition : SubscriptionCondition
{
    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; set; } = null!;
}