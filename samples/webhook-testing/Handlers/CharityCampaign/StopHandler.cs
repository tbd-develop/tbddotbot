using twitchstreambot.webhooks.Events.Channel.CharityCampaign;
using twitchstreambot.webhooks.Infrastructure;

namespace webhook_testing.Handlers.CharityCampaign;

public class StopHandler : TwitchWebhookEventHandler<Stop>
{
    public override Task Handle(PublishedEvent<Stop> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Charity campaign stopped: {@event.Message.StoppedAt} {@event.Message.CharityName}");

        return Task.CompletedTask;
    }
}