using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Events.Contracts;
using twitchstreambot.webhooks.Events.Values;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel.Chat;

[WebhookEvent("channel.chat.message", RequiredScopes = ["user:read:chat", "user:bot", "channel:bot"])]
public class ChatMessage : WebhookBaseEvent, IContainBroadcasterInformation
{
    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; set; } = null!;

    [JsonPropertyName("chatter_user_id")] public string ChatterUserId { get; set; } = null!;

    [JsonPropertyName("chatter_user_name")]
    public string ChatterUserName { get; set; } = null!;

    [JsonPropertyName("chatter_user_login")]
    public string ChatterUserLogin { get; set; } = null!;

    [JsonPropertyName("message_id")] public string MessageId { get; set; } = null!;

    public Message Message { get; set; } = null!;
    public string Color { get; set; } = null!;

    public IEnumerable<Badge> Badges { get; set; } = null!;
    [JsonPropertyName("message_type")] public string Type { get; set; } = null!;

    public object Cheer { get; set; } = null!;
    public object Reply { get; set; } = null!;

    [JsonPropertyName("channel_points_custom_reward_id")]
    public object ChannelPointsCustomRewardId { get; set; } = null!;
}