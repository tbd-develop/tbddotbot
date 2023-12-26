using twitchstreambot.webhooks.Events.Channel;
using twitchstreambot.webhooks.Infrastructure;

namespace webhook_testing.Handlers;

public class UnbanHandler : TwitchWebhookEventHandler<Unban>
{
    public override Task Handle(PublishedEvent<Unban> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Received unban for {@event.Event.UserName} by {@event.Event.ModeratorUserName}");

        return Task.CompletedTask;
    }
}