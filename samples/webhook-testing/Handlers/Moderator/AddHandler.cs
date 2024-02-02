using twitchstreambot.webhooks.Events.Channel.Moderator;
using twitchstreambot.webhooks.Infrastructure;
using twitchstreambot.webhooks.Publishing;

namespace webhook_testing.Handlers.Moderator;

public class AddHandler : TwitchWebhookEventHandler<Add>
{
    public override Task Handle(PublishedEvent<Add> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Moderator added: {@event.Message.UserName}");

        return Task.CompletedTask;
    }
}