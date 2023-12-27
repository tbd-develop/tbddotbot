using twitchstreambot.webhooks.Events.Channel.Poll;
using twitchstreambot.webhooks.Infrastructure;

namespace webhook_testing.Handlers.Poll;

public class BeginHandler : TwitchWebhookEventHandler<Begin>
{
    public override Task Handle(PublishedEvent<Begin> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Poll started: {@event.Event.Title} {@event.Event.StartedAt}");

        return Task.CompletedTask;
    }
}