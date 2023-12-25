using twitchstreambot.webhooks.Models;

namespace twitchstreambot.webhooks.Infrastructure.Contracts;

public interface IWebhookEventDispatcher
{
    Task Dispatch(
        IncomingSubscriptionMessage message,
        string data,
        CancellationToken cancellationToken = default);
}