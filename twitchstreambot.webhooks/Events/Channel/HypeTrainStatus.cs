using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Events.Contracts;
using twitchstreambot.webhooks.Events.Values;

namespace twitchstreambot.webhooks.Events.Channel;

public abstract class HypeTrainStatus : WebhookBaseEvent, IContainBroadcasterInformation
{
    public string Id { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; set; } = null!;

    public int Total { get; set; }
    public int Progress { get; set; }
    public int Goal { get; set; }

    [JsonPropertyName("top_contributions")]
    public Contribution[] TopContributions { get; set; } = null!;

    [JsonPropertyName("last_contribution")]
    public Contribution LastContribution { get; set; } = null!;

    public int Level { get; set; }
    [JsonPropertyName("started_at")] public DateTime StartedAt { get; set; }
}