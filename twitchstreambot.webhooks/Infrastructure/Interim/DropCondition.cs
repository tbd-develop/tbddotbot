using System.Text.Json.Serialization;

namespace twitchstreambot.webhooks.Infrastructure.Interim;

public class DropCondition : SubscriptionCondition
{
    [JsonPropertyName("organization_id")] public string OrganizationId { get; set; } = null!;

    [JsonPropertyName("category_id")] public string CategoryId { get; set; } = null!;

    [JsonPropertyName("campaign_id")] public string CampaignId { get; set; } = null!;
}