using twitchstreambot.webhooks.Events.Channel.Prediction;
using twitchstreambot.webhooks.Infrastructure;

namespace webhook_testing.Handlers.Prediction;

public class LockHandler : TwitchWebhookEventHandler<Lock>
{
    public override Task Handle(PublishedEvent<Lock> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Prediction locked: {@event.Message.Title} {@event.Message.LockedAt}");

        return Task.CompletedTask;
    }
}