using twitchstreambot.webhooks.Events.Stream;
using twitchstreambot.webhooks.Infrastructure;

namespace webhook_testing.Handlers.Stream;

public class OnlineHandler : TwitchWebhookEventHandler<Online>
{
    public override Task Handle(PublishedEvent<Online> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine(
            $"Stream online: {@event.Message.StartedAt} by {@event.Message.BroadcasterUserName} ({@event.Message.Type + ""})");

        return Task.CompletedTask;
    }
}