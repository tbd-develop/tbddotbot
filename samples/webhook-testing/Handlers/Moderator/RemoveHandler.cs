using twitchstreambot.webhooks.Events.Channel.Moderator;
using twitchstreambot.webhooks.Infrastructure;

namespace webhook_testing.Handlers.Moderator;

public class RemoveHandler : TwitchWebhookEventHandler<Remove>
{
    public override Task Handle(PublishedEvent<Remove> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Moderator removed: {@event.Event.UserName}");

        return Task.CompletedTask;
    }
}