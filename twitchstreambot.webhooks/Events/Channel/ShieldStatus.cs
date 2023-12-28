using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Events.Contracts;

namespace twitchstreambot.webhooks.Events.Channel;

public class ShieldStatus : WebhookBaseEvent, IContainBroadcasterInformation, IContainModeratorInformation
{
    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; set; } = null!;
    
    [JsonPropertyName("moderator_user_id")]
    public string? ModeratorUserId { get; set; }
    
    [JsonPropertyName("moderator_user_name")]
    public string? ModeratorUserName { get; set; }
    
    [JsonPropertyName("moderator_user_login")]
    public string? ModeratorUserLogin { get; set; }
}