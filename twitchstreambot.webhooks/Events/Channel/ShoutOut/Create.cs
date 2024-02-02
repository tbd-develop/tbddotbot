using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Events.Contracts;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel.ShoutOut;

[WebhookEvent("channel.shoutout.create")]
public class Create : WebhookBaseEvent,
    IContainBroadcasterInformation,
    IContainModeratorInformation
{
    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; set; } = null!;

    [JsonPropertyName("moderator_user_id")]
    public string? ModeratorUserId { get; set; }

    [JsonPropertyName("moderator_user_name")]
    public string? ModeratorUserName { get; set; }

    [JsonPropertyName("moderator_user_login")]
    public string? ModeratorUserLogin { get; set; }

    [JsonPropertyName("to_broadcaster_user_id")]
    public string ToBroadcasterUserId { get; set; } = null!;

    [JsonPropertyName("to_broadcaster_user_name")]
    public string ToBroadcasterUserName { get; set; } = null!;

    [JsonPropertyName("to_broadcaster_user_login")]
    public string ToBroadcasterUserLogin { get; set; } = null!;

    [JsonPropertyName("started_at")] public DateTime StartedAt { get; set; }

    [JsonPropertyName("viewer_count")] public int ViewerCount { get; set; }

    [JsonPropertyName("cooldown_ends_at")] public DateTime CooldownEndsAt { get; set; }

    [JsonPropertyName("target_cooldown_ends_at")]
    public DateTime TargetCooldownEndsAt { get; set; }
}