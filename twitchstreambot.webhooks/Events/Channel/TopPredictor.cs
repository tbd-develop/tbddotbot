using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Events.Contracts;

namespace twitchstreambot.webhooks.Events.Channel;

public class TopPredictor : IContainUserInformation
{
    [JsonPropertyName("user_id")] public string? UserId { get; set; }
    [JsonPropertyName("user_name")] public string? UserName { get; set; }
    [JsonPropertyName("user_login")] public string? UserLogin { get; set; }

    [JsonPropertyName("channel_points_won")]
    public int? ChannelPointsWon { get; set; }

    [JsonPropertyName("channel_points_used")]
    public int ChannelPointsUsed { get; set; }
}