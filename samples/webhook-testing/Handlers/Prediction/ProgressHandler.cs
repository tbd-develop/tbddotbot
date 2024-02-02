using twitchstreambot.webhooks.Events.Channel.Prediction;
using twitchstreambot.webhooks.Infrastructure;
using twitchstreambot.webhooks.Publishing;

namespace webhook_testing.Handlers.Prediction;

public class ProgressHandler : TwitchWebhookEventHandler<Progress>
{
    public override Task Handle(PublishedEvent<Progress> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Prediction progress: {@event.Message.Title} {@event.Message.LocksAt}");

        return Task.CompletedTask;
    }
}