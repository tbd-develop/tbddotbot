using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Events.Contracts;
using twitchstreambot.webhooks.Events.Values;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel.CharityCampaign;

[WebhookEvent("channel.charity_campaign.donate")]
public class Donate : WebhookBaseEvent, IContainBroadcasterInformation, IContainUserInformation
{
    public string Id { get; set; } = null!;

    [JsonPropertyName("campaign_id")] public string CampaignId { get; set; } = null!;

    [JsonPropertyName("charity_name")] public string CharityName { get; set; } = null!;

    [JsonPropertyName("charity_description")]
    public string CharityDescription { get; set; } = null!;

    [JsonPropertyName("charity_logo")] public string CharityLogo { get; set; } = null!;
    [JsonPropertyName("charity_website")] public string CharityWebsite { get; set; } = null!;

    public MonetaryAmount Amount { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; set; } = null!;

    [JsonPropertyName("user_id")] public string? UserId { get; set; }
    [JsonPropertyName("user_name")] public string? UserName { get; set; }
    [JsonPropertyName("user_login")] public string? UserLogin { get; set; }
}