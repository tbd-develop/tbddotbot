using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel.ChannelPointsCustomReward.Redemption;

[WebhookEvent("channel.channel_points_custom_reward_redemption.add",
    RequiredScopes = ["channel:read:redemptions", "channel:manage:redemptions"])]
public class Add : CustomRedemption
{
}