using twitchstreambot.webhooks.Events.Channel.Poll;
using twitchstreambot.webhooks.Infrastructure;

namespace webhook_testing.Handlers.Poll;

public class BeginHandler : TwitchWebhookEventHandler<Begin>
{
    public override Task Handle(PublishedEvent<Begin> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Poll started: {@event.Message.Title} {@event.Message.StartedAt}");

        return Task.CompletedTask;
    }
}