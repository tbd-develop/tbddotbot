using twitchstreambot.webhooks.Events.Extension.BitsTransaction;
using twitchstreambot.webhooks.Infrastructure;

namespace webhook_testing.Handlers.Extension;

public class BitsTransactionCreateHandler : TwitchWebhookEventHandler<Create>
{
    public override Task Handle(PublishedEvent<Create> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Received bits transaction create event: {@event.Message.Product.Name}");

        return Task.CompletedTask;
    }
}