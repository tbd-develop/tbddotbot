using twitchstreambot.webhooks.Events.Channel.Prediction;
using twitchstreambot.webhooks.Infrastructure;

namespace webhook_testing.Handlers.Prediction;

public class ProgressHandler : TwitchWebhookEventHandler<Progress>
{
    public override Task Handle(PublishedEvent<Progress> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Prediction progress: {@event.Event.Title} {@event.Event.LocksAt}");

        return Task.CompletedTask;
    }
}