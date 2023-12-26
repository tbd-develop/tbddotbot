using twitchstreambot.webhooks.Events.Channel;
using twitchstreambot.webhooks.Infrastructure;

namespace webhook_testing.Handlers;

public class RaidHandler : TwitchWebhookEventHandler<Raid>
{
    public override Task Handle(PublishedEvent<Raid> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Raid: {@event.Event.FromBroadcasterUserName} raided with {@event.Event.Viewers} viewers");
        
        return Task.CompletedTask;
    }
}