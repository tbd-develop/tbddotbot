using System.Text.Json.Serialization;

namespace twitchstreambot.webhooks.Events.Values;

public class Resub
{
    [JsonPropertyName("cumulative_months")]
    public int CumulativeMonths { get; set; }

    [JsonPropertyName("duration_months")] public int DurationMonths { get; set; }
    [JsonPropertyName("streak_months")] public int StreakMonths { get; set; }
    [JsonPropertyName("sub_tier")] public string SubTier { get; set; } = null!;
    [JsonPropertyName("is_prime")] public bool IsPrime { get; set; }
    [JsonPropertyName("is_gift")] public bool IsGift { get; set; }

    [JsonPropertyName("gifter_is_anonymous")]
    public bool GifterIsAnonymous { get; set; }

    [JsonPropertyName("gifter_user_id")] public string? GifterUserId { get; set; }

    [JsonPropertyName("gifter_user_login")]
    public string? GifterUserLogin { get; set; }

    [JsonPropertyName("gifter_user_name")] public string? GifterUserName { get; set; }
}