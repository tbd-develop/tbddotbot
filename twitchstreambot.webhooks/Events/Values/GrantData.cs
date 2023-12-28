using System.Text.Json.Serialization;

namespace twitchstreambot.webhooks.Events.Values;

public class GrantData
{
    [JsonPropertyName("organization_id")] public string OrganizationId { get; set; } = null!;
    [JsonPropertyName("category_id")] public string CategoryId { get; set; } = null!;
    [JsonPropertyName("category_name")] public string CategoryName { get; set; } = null!;
    [JsonPropertyName("campaign_id")] public string CampaignId { get; set; } = null!;
    [JsonPropertyName("user_id")] public string? UserId { get; set; }
    [JsonPropertyName("user_name")] public string? UserName { get; set; }
    [JsonPropertyName("user_login")] public string? UserLogin { get; set; }
    [JsonPropertyName("entitlement_id")] public string EntitlementId { get; set; } = null!;
    [JsonPropertyName("benefit_id")] public string BenefitId { get; set; } = null!;
    [JsonPropertyName("created_at")] public DateTime CreatedAt { get; set; }
}