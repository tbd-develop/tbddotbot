using twitchstreambot.webhooks.Events.Channel;
using twitchstreambot.webhooks.Infrastructure;

namespace webhook_testing.Handlers;

public class BanHandler : TwitchWebhookEventHandler<Ban>
{
    public override Task Handle(PublishedEvent<Ban> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Received ban for {@event.Event.UserName} with reason {@event.Event.Reason}");

        return Task.CompletedTask;
    }
}