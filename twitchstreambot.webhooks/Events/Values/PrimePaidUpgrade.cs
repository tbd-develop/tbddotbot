using System.Text.Json.Serialization;

namespace twitchstreambot.webhooks.Events.Values;

public class PrimePaidUpgrade
{
    [JsonPropertyName("sub_tier")] public SubTier SubTier { get; set; } = null!;
}