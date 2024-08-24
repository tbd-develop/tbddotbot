using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel.Prediction;

[WebhookEvent("channel.prediction.lock", RequiredScopes = ["channel:read:predictions", "channel:manage:predictions"])]
public class Lock : ChannelPrediction
{
    [JsonPropertyName("locked_at")] public DateTime LockedAt { get; set; }
}