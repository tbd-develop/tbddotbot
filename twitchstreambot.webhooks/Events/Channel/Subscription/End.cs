using twitchstreambot.Infrastructure;
using twitchstreambot.webhooks.Infrastructure;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel.Subscription;

[WebhookEvent("channel.subscription.end")]
public class End : Subscribe
{
}