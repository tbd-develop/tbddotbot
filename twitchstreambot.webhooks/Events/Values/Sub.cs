using System.Text.Json.Serialization;

namespace twitchstreambot.webhooks.Events.Values;

public class Sub
{
    [JsonPropertyName("sub_tier")] public SubTier SubTier { get; set; } = null!;
    [JsonPropertyName("is_prime")] public bool IsPrime { get; set; }
    [JsonPropertyName("duration_months")] public int DurationMonths { get; set; }
}