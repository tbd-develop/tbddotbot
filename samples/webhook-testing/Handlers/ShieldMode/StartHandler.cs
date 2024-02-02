using twitchstreambot.webhooks.Events.Channel.ShieldMode;
using twitchstreambot.webhooks.Infrastructure;
using twitchstreambot.webhooks.Publishing;

namespace webhook_testing.Handlers.ShieldMode;

public class StartHandler : TwitchWebhookEventHandler<Start>
{
    public override Task Handle(PublishedEvent<Start> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Shield mode started: {@event.Message.StartedAt} by {@event.Message.BroadcasterUserName}");

        return Task.CompletedTask;
    }
}