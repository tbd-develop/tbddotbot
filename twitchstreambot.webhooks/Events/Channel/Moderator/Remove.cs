using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Events.Contracts;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel.Moderator;

[WebhookEvent("channel.moderator.remove")]
public class Remove : WebhookBaseEvent, IContainBroadcasterInformation, IContainUserInformation
{
    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; set; }

    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; set; }

    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; set; }

    [JsonPropertyName("user_id")] public string? UserId { get; set; }
    [JsonPropertyName("user_name")] public string? UserName { get; set; }
    [JsonPropertyName("user_login")] public string? UserLogin { get; set; }
}