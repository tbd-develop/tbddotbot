using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Events.Contracts;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel;

[WebhookEvent("channel.follow", 2)]
public class Follow : WebhookBaseEvent, IContainBroadcasterInformation
{
    [JsonPropertyName("user_id")] public string UserId { get; set; } = null!;
    [JsonPropertyName("user_name")] public string UserName { get; set; } = null!;
    [JsonPropertyName("user_login")] public string UserLogin { get; set; } = null!;
    [JsonPropertyName("followed_at")] public string FollowedAt { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; set; } = null!;
}