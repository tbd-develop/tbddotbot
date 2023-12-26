using twitchstreambot.webhooks.Events.Channel;
using twitchstreambot.webhooks.Infrastructure;

namespace webhook_testing.Handlers;

public class CheerHandler : TwitchWebhookEventHandler<Cheer>
{
    public override Task Handle(PublishedEvent<Cheer> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Received cheer for {@event.Event.DisplayName} with {@event.Event.Bits} bits");

        return Task.CompletedTask;
    }
}