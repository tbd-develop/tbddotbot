using twitchstreambot.webhooks.Events;

namespace twitchstreambot.webhooks.Publishing.Contracts;

public interface ITwitchWebhookEventHandler
{
    Task Handle(PublishedEvent @event, CancellationToken cancellationToken);
}

public interface ITwitchWebhookEventHandler<TEvent> : ITwitchWebhookEventHandler
    where TEvent : WebhookBaseEvent
{
    Task Handle(PublishedEvent<TEvent> @event, CancellationToken cancellationToken);
}