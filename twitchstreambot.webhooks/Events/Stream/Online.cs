using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Events.Contracts;
using twitchstreambot.webhooks.Events.Values;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Stream;

[WebhookEvent("stream.online")]
public class Online : WebhookBaseEvent, IContainBroadcasterInformation
{
    public string Id { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; set; } = null!;

    public StreamType Type { get; set; } = null!;

    [JsonPropertyName("started_at")] public DateTime StartedAt { get; set; }
}