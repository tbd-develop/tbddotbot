using System.Text.Json.Serialization;

namespace twitchstreambot.webhooks.Events.Values;

public class MonetaryAmount
{
    public int Value { get; set; }
    [JsonPropertyName("decimal_places")] public int DecimalPlaces { get; set; }
    public string Currency { get; set; } = null!;
}