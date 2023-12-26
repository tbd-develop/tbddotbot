using twitchstreambot.webhooks.Events;

namespace twitchstreambot.webhooks.Infrastructure;

public interface ILocalEventLookup
{
    Type? FetchHandlerForEventType<TEvent>()
        where TEvent : WebhookBaseEvent;
}