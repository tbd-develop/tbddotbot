using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel.HypeTrain;

[WebhookEvent("channel.hype_train.begin")]
public class Begin : HypeTrainStatus
{
    [JsonPropertyName("expires_at")] public DateTime ExpiresAt { get; set; }
}