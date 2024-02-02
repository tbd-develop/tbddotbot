using twitchstreambot.webhooks.Events.Channel.Prediction;
using twitchstreambot.webhooks.Infrastructure;
using twitchstreambot.webhooks.Publishing;

namespace webhook_testing.Handlers.Prediction;

public class EndHandler : TwitchWebhookEventHandler<End>
{
    public override Task Handle(PublishedEvent<End> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine(
            $"Prediction ended: {@event.Message.Title} {@event.Message.EndedAt} {@event.Message.WinningOutcomeId}");

        return Task.CompletedTask;
    }
}