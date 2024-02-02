using twitchstreambot.webhooks.Events.Channel;
using twitchstreambot.webhooks.Infrastructure;
using twitchstreambot.webhooks.Publishing;

namespace webhook_testing.Handlers;

public class BanHandler : TwitchWebhookEventHandler<Ban>
{
    public override Task Handle(PublishedEvent<Ban> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Received ban for {@event.Message.UserName} with reason {@event.Message.Reason}");

        return Task.CompletedTask;
    }
}