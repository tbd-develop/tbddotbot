using twitchstreambot.webhooks.Events;

namespace twitchstreambot.webhooks.Publishing.Contracts;

public interface ILocalEventLookup
{
    IEnumerable<Type> EventTypes { get; }

    Type? FetchHandlerForEventType<TEvent>()
        where TEvent : WebhookBaseEvent;
}