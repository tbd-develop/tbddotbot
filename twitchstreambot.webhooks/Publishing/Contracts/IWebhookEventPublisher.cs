using twitchstreambot.webhooks.Events;
using twitchstreambot.webhooks.Infrastructure;

namespace twitchstreambot.webhooks.Publishing.Contracts;

public interface IWebhookEventPublisher
{
    Task Publish(WebhookBaseEvent @event, 
        TwitchHeaderCollection headers,
        CancellationToken cancellationToken = default);

    Task Publish<TEvent>(PublishedEvent<TEvent> @event, CancellationToken cancellationToken = default)
        where TEvent : WebhookBaseEvent;
}