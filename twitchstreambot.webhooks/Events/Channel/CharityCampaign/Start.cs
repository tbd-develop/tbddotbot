using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel.CharityCampaign;

[WebhookEvent("channel.charity_campaign.start")]
public class Start : CharityStatus
{
    [JsonPropertyName("started_at")] public DateTime StartedAt { get; set; }
}