using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel.Poll;

[WebhookEvent("channel.poll.end", RequiredScopes = ["channel:read:polls", "channel:manage:polls"])]
public class End : PollStatus
{
}