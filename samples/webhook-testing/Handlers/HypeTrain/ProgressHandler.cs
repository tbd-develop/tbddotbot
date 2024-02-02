using twitchstreambot.webhooks.Events.Channel.HypeTrain;
using twitchstreambot.webhooks.Infrastructure;
using twitchstreambot.webhooks.Publishing;

namespace webhook_testing.Handlers.HypeTrain;

public class ProgressHandler : TwitchWebhookEventHandler<Progress>
{
    public override Task Handle(PublishedEvent<Progress> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Hype train progress: {@event.Message.Progress} with {@event.Message.Total} points!");

        return Task.CompletedTask;
    }
}