using twitchstreambot.webhooks.Events.Channel;
using twitchstreambot.webhooks.Infrastructure;

namespace webhook_testing.Handlers;

public class RaidHandler : TwitchWebhookEventHandler<Raid>
{
    public override Task Handle(PublishedEvent<Raid> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Raid: {@event.Message.FromBroadcasterUserName} raided with {@event.Message.Viewers} viewers");
        
        return Task.CompletedTask;
    }
}