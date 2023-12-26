using System.Text.Json.Serialization;
using twitchstreambot.Infrastructure;
using twitchstreambot.webhooks.Events.Contracts;
using twitchstreambot.webhooks.Events.Values;
using twitchstreambot.webhooks.Infrastructure;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel;

[WebhookEvent("channel.subscribe")]
public class Subscribe : WebhookBaseEvent, IContainBroadcasterInformation
{
    [JsonPropertyName("user_id")] public string UserId { get; set; } = null!;
    [JsonPropertyName("user_name")] public string UserName { get; set; } = null!;
    [JsonPropertyName("user_login")] public string UserLogin { get; set; } = null!;

    public SubTier? Tier { get; set; }

    [JsonPropertyName("is_gift")] public bool IsGift { get; set; }

    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; set; } = null!;
}