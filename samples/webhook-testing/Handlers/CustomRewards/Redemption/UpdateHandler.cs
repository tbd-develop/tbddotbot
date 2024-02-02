using twitchstreambot.webhooks.Events.Channel.ChannelPointsCustomReward.Redemption;
using twitchstreambot.webhooks.Infrastructure;
using twitchstreambot.webhooks.Publishing;

namespace webhook_testing.Handlers.Redemption;

public class UpdateHandler : TwitchWebhookEventHandler<Update>
{
    public override Task Handle(PublishedEvent<Update> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Redemption updated: {@event.Message.Reward.Title}");

        return Task.CompletedTask;
    }
}