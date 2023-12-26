using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Events.Values;

namespace twitchstreambot.webhooks.Events.Channel;

public abstract class CustomReward : WebhookBaseEvent
{
    public string Id { get; set; } = null!;
    [JsonPropertyName("is_enabled")] public bool IsEnabled { get; set; }
    [JsonPropertyName("is_paused")] public bool IsPaused { get; set; }
    [JsonPropertyName("is_in_stock")] public bool IsInStock { get; set; }

    public string Title { get; set; } = null!;
    public int Cost { get; set; }
    public string Prompt { get; set; } = null!;

    [JsonPropertyName("is_user_input_required")]
    public bool IsUserInputRequired { get; set; }

    [JsonPropertyName("should_redemptions_skip_request_queue")]
    public bool ShouldRedemptionsSkipRequestQueue { get; set; }

    [JsonPropertyName("cooldown_expires_at")]
    public DateTime? CooldownExpiresAt { get; set; }

    [JsonPropertyName("max_per_stream")] public Redeem MaxPerStream { get; set; } = null!;

    [JsonPropertyName("max_per_user_per_stream")]
    public Redeem MaxPerUserPerStream { get; set; } = null!;

    [JsonPropertyName("global_cooldown")] public Redeem GlobalCooldown { get; set; } = null!;

    [JsonPropertyName("background_color")] public string BackgroundColor { get; set; } = null!;

    [JsonPropertyName("image")] public ImageUrls Image { get; set; } = null!;
    [JsonPropertyName("default_image")] public ImageUrls DefaultImage { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; set; } = null!;
}