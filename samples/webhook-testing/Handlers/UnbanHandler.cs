using twitchstreambot.webhooks.Events.Channel;
using twitchstreambot.webhooks.Infrastructure;
using twitchstreambot.webhooks.Publishing;

namespace webhook_testing.Handlers;

public class UnbanHandler : TwitchWebhookEventHandler<Unban>
{
    public override Task Handle(PublishedEvent<Unban> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Received unban for {@event.Message.UserName} by {@event.Message.ModeratorUserName}");

        return Task.CompletedTask;
    }
}