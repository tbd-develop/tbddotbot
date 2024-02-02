using twitchstreambot.webhooks.Events.Channel.ChannelPointsCustomReward.Redemption;
using twitchstreambot.webhooks.Infrastructure;
using twitchstreambot.webhooks.Publishing;

namespace webhook_testing.Handlers.Redemption;

public class AddHandler : TwitchWebhookEventHandler<Add>
{
    public override Task Handle(PublishedEvent<Add> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Redemption added: {@event.Message.Reward.Title}");

        return Task.CompletedTask;
    }
}