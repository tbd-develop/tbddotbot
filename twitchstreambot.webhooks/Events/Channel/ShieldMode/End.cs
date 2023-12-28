using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel.ShieldMode;

[WebhookEvent("channel.shield_mode.end")]
public class End : ShieldStatus
{
    [JsonPropertyName("ended_at")] public DateTime EndedAt { get; set; }
}