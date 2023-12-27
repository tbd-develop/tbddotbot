using twitchstreambot.webhooks.Events.Channel.Poll;
using twitchstreambot.webhooks.Infrastructure;

namespace webhook_testing.Handlers.Poll;

public class EndHandler : TwitchWebhookEventHandler<End>
{
    public override Task Handle(PublishedEvent<End> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Poll ended: {@event.Event.Title}");
        
        return Task.CompletedTask;
    }
}