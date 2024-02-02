using twitchstreambot.webhooks.Events;
using twitchstreambot.webhooks.Publishing.Contracts;

namespace twitchstreambot.webhooks.Publishing;

public class LocalEventLookup(IDictionary<Type, Type> lookups) : ILocalEventLookup
{
    public Type? FetchHandlerForEventType<TEvent>()
        where TEvent : WebhookBaseEvent
    {
        return lookups.TryGetValue(typeof(TEvent), out var result) ? result : null;
    }
}