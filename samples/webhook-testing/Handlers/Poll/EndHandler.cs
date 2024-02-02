using twitchstreambot.webhooks.Events.Channel.Poll;
using twitchstreambot.webhooks.Infrastructure;
using twitchstreambot.webhooks.Publishing;

namespace webhook_testing.Handlers.Poll;

public class EndHandler : TwitchWebhookEventHandler<End>
{
    public override Task Handle(PublishedEvent<End> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Poll ended: {@event.Message.Title}");
        
        return Task.CompletedTask;
    }
}