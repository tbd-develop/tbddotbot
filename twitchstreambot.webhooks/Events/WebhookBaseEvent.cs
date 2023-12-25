using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Events.Contracts;

namespace twitchstreambot.webhooks.Events;

public class WebhookBaseEvent : IContainBroadcasterInformation
{
    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; set; } = null!;
}