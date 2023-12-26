using System.Text.Json.Serialization;

namespace twitchstreambot.webhooks.Events.Values;

public class Redeem
{
    [JsonPropertyName("is_enabled")] public bool IsEnabled { get; set; }
    public int Value { get; set; }
}