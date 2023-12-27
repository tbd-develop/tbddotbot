using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Events.Contracts;
using twitchstreambot.webhooks.Events.Values;

namespace twitchstreambot.webhooks.Events.Channel;

public class CharityStatus : WebhookBaseEvent, IContainBroadcasterInformation
{
    public string Id { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; set; } = null!;

    [JsonPropertyName("charity_name")] public string CharityName { get; set; } = null!;

    [JsonPropertyName("charity_description")]
    public string CharityDescription { get; set; } = null!;

    [JsonPropertyName("charity_logo")] public string CharityLogo { get; set; } = null!;
    [JsonPropertyName("charity_website")] public string CharityWebsite { get; set; } = null!;

    [JsonPropertyName("current_amount")] public MonetaryAmount CurrentAmount { get; set; } = null!;
    [JsonPropertyName("target_amount")] public MonetaryAmount TargetAmount { get; set; } = null!;
}