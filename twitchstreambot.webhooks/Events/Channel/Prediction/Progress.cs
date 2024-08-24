using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel.Prediction;

[WebhookEvent("channel.prediction.progress", RequiredScopes = ["channel:read:predictions", "channel:manage:predictions"])]
public class Progress : ChannelPrediction
{
    [JsonPropertyName("locks_at")]
    public DateTime LocksAt { get; set; }
}