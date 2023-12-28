using twitchstreambot.webhooks.Events.Channel.ShoutOut;
using twitchstreambot.webhooks.Infrastructure;

namespace webhook_testing.Handlers.Shoutout;

public class ReceiveHandler : TwitchWebhookEventHandler<Receive>
{
    public override Task Handle(PublishedEvent<Receive> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Shoutout received: {@event.Message.StartedAt} by {@event.Message.BroadcasterUserName}");

        return Task.CompletedTask;
    }
}