using twitchstreambot.webhooks.Events.Stream;
using twitchstreambot.webhooks.Infrastructure;
using twitchstreambot.webhooks.Publishing;

namespace webhook_testing.Handlers.Stream;

public class OfflineHandler : TwitchWebhookEventHandler<Offline>
{
    public override Task Handle(PublishedEvent<Offline> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Stream offline: {@event.Message.BroadcasterUserName}");

        return Task.CompletedTask;
    }
}