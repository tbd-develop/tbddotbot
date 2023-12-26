using System.Text.Json.Serialization;

namespace twitchstreambot.webhooks.Events.Values;

public class ImageUrls
{
    [JsonPropertyName("url_1x")] public string? Url1X { get; set; }
    [JsonPropertyName("url_2x")] public string? Url2X { get; set; }
    [JsonPropertyName("url_4x")] public string? Url4X { get; set; }
}