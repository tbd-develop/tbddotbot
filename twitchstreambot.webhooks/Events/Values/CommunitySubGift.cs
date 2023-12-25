using System.Text.Json.Serialization;

namespace twitchstreambot.webhooks.Events.Values;

public class CommunitySubGift
{
    public string Id { get; set; } = null!;
    public int Total { get; set; }
    [JsonPropertyName("sub_tier")] public SubTier SubTier { get; set; } = null!;
    [JsonPropertyName("cumulative_total")] public int CumulativeTotal { get; set; }
}