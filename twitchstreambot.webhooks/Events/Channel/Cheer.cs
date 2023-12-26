using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Events.Contracts;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel;

[WebhookEvent("channel.cheer")]
public class Cheer : WebhookBaseEvent,
    IContainBroadcasterInformation,
    IContainUserInformation
{
    [JsonPropertyName("user_id")] public string? UserId { get; set; }
    [JsonPropertyName("user_name")] public string? UserName { get; set; }
    [JsonPropertyName("user_login")] public string? UserLogin { get; set; }

    [JsonPropertyName("is_anonymous")] public bool IsAnonymous { get; set; }

    public string? Message { get; set; }
    public int Bits { get; set; }

    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; set; } = null!;
}