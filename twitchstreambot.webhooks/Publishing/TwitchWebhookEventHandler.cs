using twitchstreambot.webhooks.Events;
using twitchstreambot.webhooks.Publishing.Contracts;

namespace twitchstreambot.webhooks.Publishing;

public abstract class TwitchWebhookEventHandler<TEvent> : ITwitchWebhookEventHandler<TEvent>
    where TEvent : WebhookBaseEvent
{
    Task ITwitchWebhookEventHandler.Handle(PublishedEvent @event, CancellationToken cancellationToken)
    {
        if (@event is PublishedEvent<TEvent> typedEvent)
            return Handle(typedEvent, cancellationToken);

        throw new ArgumentException($"Expected event of type {typeof(TEvent).Name} but got {@event.GetType().Name}");
    }

    public abstract Task Handle(PublishedEvent<TEvent> @event, CancellationToken cancellationToken);
}