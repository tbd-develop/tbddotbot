using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel.ChannelPointsCustomReward.Redemption;

[WebhookEvent("channel.channel_points_custom_reward_redemption.add")]
public class Add : CustomRedemption
{
}