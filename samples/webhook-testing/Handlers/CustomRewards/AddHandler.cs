using twitchstreambot.webhooks.Events.Channel.ChannelPointsCustomReward;
using twitchstreambot.webhooks.Infrastructure;

namespace webhook_testing.Handlers;

public class AddHandler : TwitchWebhookEventHandler<Add>
{
    public override Task Handle(PublishedEvent<Add> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Channel points reward added: {@event.Event.Title}");

        return Task.CompletedTask;
    }
}