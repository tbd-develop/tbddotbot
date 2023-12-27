using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel.Poll;

[WebhookEvent("channel.poll.progress")]
public class Progress : PollStatus
{
}