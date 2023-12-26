using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Events.Contracts;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel;

[WebhookEvent("channel.unban")]
public class Unban : WebhookBaseEvent,
    IContainBroadcasterInformation,
    IContainUserInformation,
    IContainModeratorInformation
{
    [JsonPropertyName("user_id")] public string? UserId { get; set; }
    [JsonPropertyName("user_name")] public string? UserName { get; set; }
    [JsonPropertyName("user_login")] public string? UserLogin { get; set; }


    [JsonPropertyName("moderator_user_id")]
    public string? ModeratorUserId { get; set; }

    [JsonPropertyName("moderator_user_name")]
    public string? ModeratorUserName { get; set; }

    [JsonPropertyName("moderator_user_login")]
    public string? ModeratorUserLogin { get; set; }

    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; set; } = null!;
}