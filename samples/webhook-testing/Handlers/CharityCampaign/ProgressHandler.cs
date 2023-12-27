using twitchstreambot.webhooks.Events.Channel.CharityCampaign;
using twitchstreambot.webhooks.Infrastructure;

namespace webhook_testing.Handlers.CharityCampaign;

public class ProgressHandler : TwitchWebhookEventHandler<Progress>
{
    public override Task Handle(PublishedEvent<Progress> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine(
            $"Charity campaign progress: {@event.Message.CharityName} {@event.Message.CurrentAmount?.Currency} {@event.Message.CurrentAmount?.Value}");

        return Task.CompletedTask;
    }
}