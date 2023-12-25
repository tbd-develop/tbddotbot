using System.Text.Json.Serialization;

namespace twitchstreambot.webhooks.Events.Values;

public class SubGift
{
    [JsonPropertyName("duration_months")] public int DurationMonths { get; set; }
    [JsonPropertyName("cumulative_total")] public int CumulativeTotal { get; set; }

    [JsonPropertyName("recipient_user_id")]
    public string RecipientUserId { get; set; } = null!;

    [JsonPropertyName("recipient_user_name")]
    public string RecipientUserName { get; set; } = null!;

    [JsonPropertyName("recipient_user_login")]
    public string RecipientUserLogin { get; set; } = null!;

    [JsonPropertyName("sub_tier")] public SubTier SubTier { get; set; } = null!;

    [JsonPropertyName("community_gift_id")]
    public string CommunityGiftId { get; set; } = null!;
}