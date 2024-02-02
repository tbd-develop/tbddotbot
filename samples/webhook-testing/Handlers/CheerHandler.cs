using twitchstreambot.webhooks.Events.Channel;
using twitchstreambot.webhooks.Infrastructure;
using twitchstreambot.webhooks.Publishing;

namespace webhook_testing.Handlers;

public class CheerHandler : TwitchWebhookEventHandler<Cheer>
{
    public override Task Handle(PublishedEvent<Cheer> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Received cheer for {@event.Message.UserName} with {@event.Message.Bits} bits");

        return Task.CompletedTask;
    }
}