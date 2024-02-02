using twitchstreambot.webhooks.Events.Channel.ChannelPointsCustomReward;
using twitchstreambot.webhooks.Infrastructure;
using twitchstreambot.webhooks.Publishing;

namespace webhook_testing.Handlers;

public class UpdateHandler : TwitchWebhookEventHandler<Update>
{
    public override Task Handle(PublishedEvent<Update> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Channel points reward updated: {@event.Message.Title}");

        return Task.CompletedTask;
    }
}