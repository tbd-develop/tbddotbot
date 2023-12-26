using System.Text.Json.Serialization;
using twitchstreambot.Infrastructure;
using twitchstreambot.webhooks.Infrastructure;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel;

[WebhookEvent("channel.follow", 2)]
public class Follow : WebhookFromBroadcasterEvent
{
    [JsonPropertyName("user_id")] public string UserId { get; set; } = null!;
    [JsonPropertyName("user_name")] public string UserName { get; set; } = null!;
    [JsonPropertyName("user_login")] public string UserLogin { get; set; } = null!;
    [JsonPropertyName("followed_at")] public string FollowedAt { get; set; } = null!;
}