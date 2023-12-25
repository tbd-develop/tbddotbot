using System.Text.Json.Serialization;

namespace twitchstreambot.webhooks.Events.Values;

public class BitsBadgeTier
{
    [JsonPropertyName("tier")]
    public int Tier { get; set; }
}