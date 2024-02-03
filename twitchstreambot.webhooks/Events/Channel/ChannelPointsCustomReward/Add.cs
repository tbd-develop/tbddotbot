using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel.ChannelPointsCustomReward;

[WebhookEvent("channel.channel_points_custom_reward.add",
    RequiredScopes = ["channel:read:redemptions", "channel:manage:redemptions"])]
public class Add : CustomReward
{
}