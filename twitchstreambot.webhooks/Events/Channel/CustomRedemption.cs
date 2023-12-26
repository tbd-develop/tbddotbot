using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Events.Contracts;
using twitchstreambot.webhooks.Events.Values;

namespace twitchstreambot.webhooks.Events.Channel;

public class CustomRedemption : WebhookBaseEvent, IContainBroadcasterInformation, IContainUserInformation
{
    [JsonPropertyName("id")] public string Id { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; set; } = null!;

    [JsonPropertyName("user_id")] public string? UserId { get; set; }
    [JsonPropertyName("user_name")] public string? UserName { get; set; }
    [JsonPropertyName("user_login")] public string? UserLogin { get; set; }

    public string Status { get; set; } = null!;
    public Reward Reward { get; set; } = null!;
    [JsonPropertyName("redeemed_at")] public DateTime RedeemedAt { get; set; }
}