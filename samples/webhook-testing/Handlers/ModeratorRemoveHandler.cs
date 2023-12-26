using twitchstreambot.webhooks.Events.Channel.Moderator;
using twitchstreambot.webhooks.Infrastructure;

namespace webhook_testing.Handlers;

public class ModeratorRemoveHandler : TwitchWebhookEventHandler<Remove>
{
    public override Task Handle(PublishedEvent<Remove> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Moderator removed: {@event.Event.UserName}");

        return Task.CompletedTask;
    }
}