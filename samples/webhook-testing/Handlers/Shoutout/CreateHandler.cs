using twitchstreambot.webhooks.Events.Channel.ShoutOut;
using twitchstreambot.webhooks.Infrastructure;
using twitchstreambot.webhooks.Publishing;

namespace webhook_testing.Handlers.Shoutout;

public class CreateHandler : TwitchWebhookEventHandler<Create>
{
    public override Task Handle(PublishedEvent<Create> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Shoutout created: {@event.Message.StartedAt} by {@event.Message.BroadcasterUserName}");

        return Task.CompletedTask;
    }
}