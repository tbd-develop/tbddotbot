using twitchstreambot.webhooks.Events.Channel.Poll;
using twitchstreambot.webhooks.Infrastructure;

namespace webhook_testing.Handlers.Poll;

public class ProgressHandler : TwitchWebhookEventHandler<Progress>
{
    public override Task Handle(PublishedEvent<Progress> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Poll progress: {@event.Message.Title} {@event.Message.StartedAt}");

        return Task.CompletedTask;
    }
}