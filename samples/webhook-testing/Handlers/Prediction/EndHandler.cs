using twitchstreambot.webhooks.Events.Channel.Prediction;
using twitchstreambot.webhooks.Infrastructure;

namespace webhook_testing.Handlers.Prediction;

public class EndHandler : TwitchWebhookEventHandler<End>
{
    public override Task Handle(PublishedEvent<End> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine(
            $"Prediction ended: {@event.Event.Title} {@event.Event.EndedAt} {@event.Event.WinningOutcomeId}");

        return Task.CompletedTask;
    }
}