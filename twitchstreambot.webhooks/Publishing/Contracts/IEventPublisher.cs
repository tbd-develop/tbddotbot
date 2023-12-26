using twitchstreambot.webhooks.Events;

namespace twitchstreambot.webhooks.Infrastructure;

public interface IEventPublisher
{
    Task Publish(WebhookBaseEvent @event, TwitchHeaderCollection headers,
        CancellationToken cancellationToken = default);

    Task Publish<TEvent>(PublishedEvent<TEvent> @event, CancellationToken cancellationToken = default)
        where TEvent : WebhookBaseEvent;
}