using twitchstreambot.webhooks.Events.Channel.ShieldMode;
using twitchstreambot.webhooks.Infrastructure;
using twitchstreambot.webhooks.Publishing;

namespace webhook_testing.Handlers.ShieldMode;

public class EndHandler : TwitchWebhookEventHandler<End>
{
    public override Task Handle(PublishedEvent<End> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Shield mode ended: {@event.Message.EndedAt} by {@event.Message.BroadcasterUserName}");

        return Task.CompletedTask;
    }
}