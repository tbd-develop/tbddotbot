using twitchstreambot.webhooks.Events.Channel.HypeTrain;
using twitchstreambot.webhooks.Infrastructure;
using twitchstreambot.webhooks.Publishing;

namespace webhook_testing.Handlers.HypeTrain;

public class BeginHandler : TwitchWebhookEventHandler<Begin>
{
    public override Task Handle(PublishedEvent<Begin> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Hype train started with {@event.Message.StartedAt} with {@event.Message.Goal} points!");

        return Task.CompletedTask;
    }
}