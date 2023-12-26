using twitchstreambot.webhooks.Events;

namespace twitchstreambot.webhooks.Infrastructure;

public class LocalEventLookup(IDictionary<Type, Type> lookups) : ILocalEventLookup
{
    public Type? FetchHandlerForEventType<TEvent>()
        where TEvent : WebhookBaseEvent
    {
        return lookups.TryGetValue(typeof(TEvent), out var result) ? result : null;
    }
}