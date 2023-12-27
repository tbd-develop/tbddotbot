using System.Text.Json.Serialization;

namespace twitchstreambot.webhooks.Events.Values;

public class VotingStatus
{
    [JsonPropertyName("is_enabled")] public bool IsEnabled { get; set; }
    [JsonPropertyName("amount_per_vote")] public int AmountPerVote { get; set; }
}