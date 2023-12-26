using twitchstreambot.webhooks.Events.Channel.ChannelPointsCustomReward.Redemption;
using twitchstreambot.webhooks.Infrastructure;

namespace webhook_testing.Handlers.Redemption;

public class AddHandler : TwitchWebhookEventHandler<Add>
{
    public override Task Handle(PublishedEvent<Add> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Redemption added: {@event.Event.Reward.Title}");

        return Task.CompletedTask;
    }
}