using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Events.Contracts;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel;

[WebhookEvent("channel.ban")]
public class Ban : WebhookFromBroadcasterEvent, IContainUserInformation
{
    [JsonPropertyName("user_id")] public string? UserId { get; set; } = null!;
    [JsonPropertyName("user_name")] public string? UserName { get; set; } = null!;
    [JsonPropertyName("user_login")] public string? UserLogin { get; set; } = null!;

    [JsonPropertyName("moderator_user_id")]
    public string? ModeratorUserId { get; set; } = null!;

    [JsonPropertyName("moderator_user_login")]
    public string? ModeratorUserLogin { get; set; } = null!;

    [JsonPropertyName("moderator_user_name")]
    public string? ModeratorUserName { get; set; } = null!;

    public string? Reason { get; set; } = null!;
    [JsonPropertyName("banned_at")] public DateTime BannedAt { get; set; }
    [JsonPropertyName("ends_at")] public DateTime? EndsAt { get; set; }

    [JsonPropertyName("is_permanent")] public bool IsPermanent { get; set; }
}