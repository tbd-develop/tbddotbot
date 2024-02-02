using twitchstreambot.webhooks.Events;

namespace twitchstreambot.webhooks.Publishing.Contracts;

public interface ILocalEventLookup
{
    Type? FetchHandlerForEventType<TEvent>()
        where TEvent : WebhookBaseEvent;
}