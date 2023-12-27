using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel.HypeTrain;

[WebhookEvent("channel.hype_train.progress")]
public class Progress : HypeTrainStatus
{
    [JsonPropertyName("expires_at")] public DateTime ExpiresAt { get; set; }
    
}