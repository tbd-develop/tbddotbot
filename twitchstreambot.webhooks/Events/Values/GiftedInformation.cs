using System.Text.Json.Serialization;

namespace twitchstreambot.webhooks.Events.Values;

public class GiftedInformation
{
    [JsonPropertyName("gifter_is_anonymous")]
    public bool GifterIsAnonymous { get; set; }
    [JsonPropertyName("gifter_user_id")] public string? GifterUserId { get; set; }

    [JsonPropertyName("gifter_user_login")]
    public string? GifterUserLogin { get; set; }

    [JsonPropertyName("gifter_user_name")] public string? GifterUserName { get; set; }
}