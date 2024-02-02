using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Infrastructure.Interim;

namespace twitchstreambot.webhooks.Api.Response;

public class WebhookSubscriptionContent
{
    public string Id { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string Version { get; set; } = null!;
    [JsonPropertyName("created_at ")] public string CreatedAt { get; set; } = null!;
    public decimal? Cost { get; set; }
    public string ExpiresAt { get; set; } = null!;
}

public class WebhookSubscriptionContent<TSubscriptionCondition, TTransportDefinition> : WebhookSubscriptionContent
    where TSubscriptionCondition : SubscriptionCondition
    where TTransportDefinition : SubscriptionTransportDefinition
{
    public TSubscriptionCondition Condition { get; set; } = null!;
    public TTransportDefinition Transport { get; set; } = null!;
}