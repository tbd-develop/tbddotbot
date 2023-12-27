using twitchstreambot.webhooks.Events.Channel.CharityCampaign;
using twitchstreambot.webhooks.Infrastructure;

namespace webhook_testing.Handlers.CharityCampaign;

public class StartHandler : TwitchWebhookEventHandler<Start>
{
    public override Task Handle(PublishedEvent<Start> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Charity campaign started: {@event.Message.StartedAt} {@event.Message.CharityName}");

        return Task.CompletedTask;
    }
}