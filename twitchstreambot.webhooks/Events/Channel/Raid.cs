using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel;

[WebhookEvent("channel.raid")]
public class Raid : WebhookBaseEvent
{
    [JsonPropertyName("from_broadcaster_user_id")]
    public string FromBroadcasterUserId { get; set; } = null!;

    [JsonPropertyName("from_broadcaster_user_login")]
    public string FromBroadcasterUserLogin { get; set; } = null!;

    [JsonPropertyName("from_broadcaster_user_name")]
    public string FromBroadcasterUserName { get; set; } = null!;

    [JsonPropertyName("to_broadcaster_user_id")]
    public string ToBroadcasterUserId { get; set; } = null!;

    [JsonPropertyName("to_broadcaster_user_login")]
    public string ToBroadcasterUserLogin { get; set; } = null!;

    [JsonPropertyName("to_broadcaster_user_name")]
    public string ToBroadcasterUserName { get; set; } = null!;

    public int Viewers { get; set; }
}