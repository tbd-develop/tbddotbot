using System.Text.Json.Serialization;

namespace twitchstreambot.webhooks.Events.Values;

public class Emote
{
    public string Id { get; set; } = null!;
    [JsonPropertyName("emote_set_id")] public string EmoteSetId { get; set; } = null!;
    [JsonPropertyName("owner_id")] public string OwnerId { get; set; } = null!;
    public string[] Format { get; set; } = null!;
}