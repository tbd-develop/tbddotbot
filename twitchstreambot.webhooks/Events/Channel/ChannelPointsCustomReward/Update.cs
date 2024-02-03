using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel.ChannelPointsCustomReward;

[WebhookEvent("channel.channel_points_custom_reward.update",
    RequiredScopes = ["channel:read:redemptions", "channel:manage:redemptions"])]
public class Update : CustomReward
{
}