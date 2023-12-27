using twitchstreambot.webhooks.Events.Channel.Prediction;
using twitchstreambot.webhooks.Infrastructure;

namespace webhook_testing.Handlers.Prediction;

public class BeginHandler : TwitchWebhookEventHandler<Begin>
{
    public override Task Handle(PublishedEvent<Begin> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Prediction started: {@event.Event.Title} {@event.Event.LocksAt}");

        return Task.CompletedTask;
    }
}