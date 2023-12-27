using twitchstreambot.webhooks.Events.Channel.HypeTrain;
using twitchstreambot.webhooks.Infrastructure;

namespace webhook_testing.Handlers.HypeTrain;

public class EndHandler : TwitchWebhookEventHandler<End>
{
    public override Task Handle(PublishedEvent<End> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Hype train ended with {@event.Message.Total} at {@event.Message.CooldownEndsAt}!");

        return Task.CompletedTask;
    }
}