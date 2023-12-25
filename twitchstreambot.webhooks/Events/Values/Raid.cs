using System.Text.Json.Serialization;

namespace twitchstreambot.webhooks.Events.Values;

public class Raid
{
    [JsonPropertyName("user_id")] public string UserId { get; set; } = null!;
    [JsonPropertyName("user_name")] public string UserName { get; set; } = null!;
    [JsonPropertyName("user_login")] public string UserLogin { get; set; } = null!;
    [JsonPropertyName("viewer_count")] public int ViewerCount { get; set; }

    [JsonPropertyName("profile_image_url")]
    public string ProfileImageUrl { get; set; } = null!;
}