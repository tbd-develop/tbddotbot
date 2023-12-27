using twitchstreambot.webhooks.Events.Channel.CharityCampaign;
using twitchstreambot.webhooks.Infrastructure;

namespace webhook_testing.Handlers.CharityCampaign;

public class DonateHandler : TwitchWebhookEventHandler<Donate>
{
    public override Task Handle(PublishedEvent<Donate> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Charity campaign donation: {@event.Message.Amount.Value} {@event.Message.Amount.Currency}");

        return Task.CompletedTask;
    }
}