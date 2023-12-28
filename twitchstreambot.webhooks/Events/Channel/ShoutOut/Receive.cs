using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Events.Contracts;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel.ShoutOut;

[WebhookEvent("channel.shoutout.receive")]
public class Receive : WebhookBaseEvent, IContainBroadcasterInformation
{
    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; set; } = null!;

    [JsonPropertyName("from_broadcaster_user_id")]
    public string FromBroadcasterUserId { get; set; } = null!;

    [JsonPropertyName("from_broadcaster_user_name")]
    public string FromBroadcasterUserName { get; set; } = null!;

    [JsonPropertyName("from_broadcaster_user_login")]
    public string FromBroadcasterUserLogin { get; set; } = null!;

    [JsonPropertyName("viewer_count")] public int ViewerCount { get; set; }

    [JsonPropertyName("started_at")] public DateTime StartedAt { get; set; }
}