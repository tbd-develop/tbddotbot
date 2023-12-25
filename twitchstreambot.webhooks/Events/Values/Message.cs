using System.Text.Json.Serialization;

namespace twitchstreambot.webhooks.Events.Values;

public class Message
{
    [JsonPropertyName("text")] public string Text { get; set; } = null!;

    [JsonPropertyName("fragments")] public Fragment[] Fragments { get; set; } = null!;
}