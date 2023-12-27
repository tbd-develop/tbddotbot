using twitchstreambot.webhooks.Events.Channel.ChannelPointsCustomReward;
using twitchstreambot.webhooks.Infrastructure;

namespace webhook_testing.Handlers;

public class RemoveHandler : TwitchWebhookEventHandler<Remove>
{
    public override Task Handle(PublishedEvent<Remove> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Channel points reward removed: {@event.Message.Title}");

        return Task.CompletedTask;
    }
}