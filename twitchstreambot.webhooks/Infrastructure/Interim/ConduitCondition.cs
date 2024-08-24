using System.Text.Json.Serialization;

namespace twitchstreambot.webhooks.Infrastructure.Interim;

public class ConduitCondition : SubscriptionCondition
{
    [JsonPropertyName("client_id")] public string ClientId { get; set; } = null!;

    [JsonPropertyName("conduit_id")] public string ConduitId { get; set; } = null!;
}