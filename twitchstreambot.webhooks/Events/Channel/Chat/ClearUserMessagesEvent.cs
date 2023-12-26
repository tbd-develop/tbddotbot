using System.Text.Json.Serialization;
using twitchstreambot.Infrastructure;
using twitchstreambot.webhooks.Events.Contracts;
using twitchstreambot.webhooks.Infrastructure;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel.Chat;

[WebhookEvent("channel.chat.clear_user_messages")]
public class ClearUserMessagesEvent : WebhookBaseEvent, IContainBroadcasterInformation
{
    [JsonPropertyName("target_user_id")] public string TargetUserId { get; set; } = null!;

    [JsonPropertyName("target_user_login")]
    public string TargetUserLogin { get; set; } = null!;

    [JsonPropertyName("target_user_name")] public string TargetUserName { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; set; }

    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; set; }

    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; set; }
}