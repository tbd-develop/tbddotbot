using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel.Prediction;

[WebhookEvent("channel.prediction.end")]
public class End : ChannelPrediction
{
    [JsonPropertyName("winning_outcome_id")]
    public string WinningOutcomeId { get; set; } = null!;

    public string Status { get; set; } = null!;
    
    [JsonPropertyName("ended_at")] public DateTime EndedAt { get; set; }
}