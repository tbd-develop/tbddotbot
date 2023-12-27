using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel.Prediction;

[WebhookEvent("channel.prediction.lock")]
public class Lock : ChannelPrediction
{
    [JsonPropertyName("locked_at")] public DateTime LockedAt { get; set; }
}