using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel.ShieldMode;

[WebhookEvent("channel.shield_mode.start")]
public class Start : ShieldStatus
{
    [JsonPropertyName("started_at")] public DateTime StartedAt { get; set; }
}