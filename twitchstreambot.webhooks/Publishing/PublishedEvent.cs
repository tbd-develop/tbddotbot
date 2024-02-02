using twitchstreambot.webhooks.Events;
using twitchstreambot.webhooks.Infrastructure;

namespace twitchstreambot.webhooks.Publishing;

public abstract class PublishedEvent(TwitchHeaderCollection headers)
{
    public TwitchHeaderCollection Headers { get; set; } = headers;
    public object Event { get; set; } = default!;
}

public class PublishedEvent<TEvent>(TEvent message, TwitchHeaderCollection headers) : PublishedEvent(headers)
    where TEvent : WebhookBaseEvent
{
    public new TEvent Message { get; } = message;
}