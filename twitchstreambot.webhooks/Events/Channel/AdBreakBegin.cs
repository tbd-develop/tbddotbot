using System.Text.Json.Serialization;
using twitchstreambot.Infrastructure;
using twitchstreambot.webhooks.Events.Contracts;
using twitchstreambot.webhooks.Infrastructure;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel;

[WebhookEvent("channel.ad_break.begin")]
public class AdBreakBegin : WebhookBaseEvent,
    IContainBroadcasterInformation
{
    [JsonPropertyName("requester_user_id")]
    public string RequesterUserId { get; set; } = null!;

    [JsonPropertyName("requester_user_login")]
    public string RequesterUserLogin { get; set; } = null!;

    [JsonPropertyName("requester_user_name")]
    public string RequesterUserName { get; set; } = null!;

    [JsonPropertyName("duration_seconds")] public string DurationSeconds { get; set; } = null!;
    public int DurationSecondsInt => int.Parse(DurationSeconds);

    [JsonPropertyName("started_at")] public DateTime? StartedAt { get; set; }
    [JsonPropertyName("is_automatic")] public string IsAutomaticString { get; set; } = null!;
    public bool IsAutomatic => bool.TryParse(IsAutomaticString, out var result) && result;

    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; set; } = null!;
}