using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel.CharityCampaign;

[WebhookEvent("channel.charity_campaign.progress", RequiredScopes = ["channel:read:charity"])]
public class Progress : CharityStatus
{
}