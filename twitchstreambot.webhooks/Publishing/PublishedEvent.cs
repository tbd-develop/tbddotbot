using twitchstreambot.webhooks.Events;

namespace twitchstreambot.webhooks.Infrastructure;

public abstract class PublishedEvent(TwitchHeaderCollection headers)
{
    public TwitchHeaderCollection Headers { get; set; } = headers;
    public object Event { get; set; } = default!;
}

public class PublishedEvent<TEvent>(TEvent @event, TwitchHeaderCollection headers) : PublishedEvent(headers)
    where TEvent : WebhookBaseEvent
{
    public new TEvent Event { get; } = @event;
}