using twitchstreambot.webhooks.Events.Channel.Prediction;
using twitchstreambot.webhooks.Infrastructure;
using twitchstreambot.webhooks.Publishing;

namespace webhook_testing.Handlers.Prediction;

public class BeginHandler : TwitchWebhookEventHandler<Begin>
{
    public override Task Handle(PublishedEvent<Begin> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Prediction started: {@event.Message.Title} {@event.Message.LocksAt}");

        return Task.CompletedTask;
    }
}