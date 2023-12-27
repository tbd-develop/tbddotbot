using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Events.Contracts;

namespace twitchstreambot.webhooks.Events.Channel;

public class ChannelPrediction : WebhookBaseEvent, IContainBroadcasterInformation
{
    public string Id { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; set; } = null!;

    public string Title { get; set; } = null!;
    public PredictionOutcome[] Outcomes { get; set; } = null!;
    [JsonPropertyName("started_at")]
    public DateTime StartedAt { get; set; }
}