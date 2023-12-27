using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel.CharityCampaign;

[WebhookEvent("channel.charity_campaign.stop")]
public class Stop : CharityStatus
{
    [JsonPropertyName("stopped_at")] public DateTime StoppedAt { get; set; }
}