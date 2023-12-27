using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Events.Contracts;
using twitchstreambot.webhooks.Events.Values;

namespace twitchstreambot.webhooks.Events.Channel;

public class PollStatus : WebhookBaseEvent, IContainBroadcasterInformation
{
    public string Id { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; set; } = null!;

    public string Title { get; set; } = null!;

    public IEnumerable<Choice> Choices { get; set; } = null!;
    [JsonPropertyName("bits_voting")] public VotingStatus? BitsVoting { get; set; }

    [JsonPropertyName("channel_points_voting")]
    public VotingStatus? ChannelPointsVoting { get; set; }

    [JsonPropertyName("started_at")] public DateTime StartedAt { get; set; }

    [JsonPropertyName("ends_at")] public DateTime EndsAt { get; set; }
}