using System.Text.Json.Serialization;

namespace twitchstreambot.pubsub.Messages;

public class TwitchUserResponse
{
    public long Id { get; set; }
    public string Login { get; set; } = null!;
    [JsonPropertyName("display_name")] public string DisplayName { get; set; } = null!;
}