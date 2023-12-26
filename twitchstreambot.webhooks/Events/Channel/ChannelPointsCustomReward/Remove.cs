using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel.ChannelPointsCustomReward;

[WebhookEvent("channel.channel_points_custom_reward.remove")]
public class Remove : CustomReward
{
}