using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel.Poll;

[WebhookEvent("channel.poll.end")]
public class End : PollStatus
{
}