using System.Text.Json.Serialization;

namespace twitchstreambot.webhooks.Events.Values;

public class BitsExtensionProduct
{
    public string Name { get; set; } = null!;
    public string Sku { get; set; } = null!;
    public int Bits { get; set; }
    
    [JsonPropertyName("in_development")] public bool InDevelopment { get; set; }
}