using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel.Prediction;

[WebhookEvent("channel.prediction.begin")]
public class Begin : ChannelPrediction
{
    [JsonPropertyName("locks_at")] public DateTime LocksAt { get; set; }
}