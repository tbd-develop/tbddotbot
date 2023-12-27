using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Events.Contracts;

namespace twitchstreambot.webhooks.Events.Values;

public class Contribution : IContainUserInformation
{
    [JsonPropertyName("user_id")] public string? UserId { get; set; }
    [JsonPropertyName("user_name")] public string? UserName { get; set; }
    [JsonPropertyName("user_login")] public string? UserLogin { get; set; }
    public ContributionType Type { get; set; } = ContributionType.Other;
    public int Total { get; set; }
}