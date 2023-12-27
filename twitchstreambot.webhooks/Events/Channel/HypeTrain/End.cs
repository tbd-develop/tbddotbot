using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel.HypeTrain;

[WebhookEvent("channel.hype_train.end")]
public class End : HypeTrainStatus
{
    [JsonPropertyName("cooldown_ends_at")] public DateTime CooldownEndsAt { get; set; }
}