using twitchstreambot.webhooks.Events.Channel.Moderator;
using twitchstreambot.webhooks.Infrastructure;
using twitchstreambot.webhooks.Publishing;

namespace webhook_testing.Handlers.Moderator;

public class RemoveHandler : TwitchWebhookEventHandler<Remove>
{
    public override Task Handle(PublishedEvent<Remove> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Moderator removed: {@event.Message.UserName}");

        return Task.CompletedTask;
    }
}