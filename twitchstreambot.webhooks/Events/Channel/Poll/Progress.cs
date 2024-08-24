using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel.Poll;

[WebhookEvent("channel.poll.progress", RequiredScopes = ["channel:read:polls", "channel:manage:polls"])]
public class Progress : PollStatus
{
}