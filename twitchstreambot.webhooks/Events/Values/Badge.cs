using System.Text.Json.Serialization;

namespace twitchstreambot.webhooks.Events.Values;

public class Badge
{
    [JsonPropertyName("set_id")] public string SetId { get; set; } = null!;

    public string Id { get; set; } = null!;

    public string Info { get; set; } = null!;
}