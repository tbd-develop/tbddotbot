using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using twitchstreambot.webhooks.Events;

namespace twitchstreambot.webhooks.Infrastructure;

public class LocalEventPublisher(
    ILocalEventLookup lookup,
    IServiceProvider provider) : IEventPublisher
{
    public Task? Publish(WebhookBaseEvent @event, TwitchHeaderCollection headers,
        CancellationToken cancellationToken = default)
    {
        var eventType = @event.GetType();

        var method = typeof(LocalEventPublisher)
            .GetMethod(nameof(BuildAndPublish), BindingFlags.NonPublic | BindingFlags.Instance)!;

        var generic = method.MakeGenericMethod(eventType);

        return (Task)generic.Invoke(this, new object[] { @event, headers, cancellationToken })!;
    }

    private async Task BuildAndPublish<TEvent>(TEvent @event, TwitchHeaderCollection headers,
        CancellationToken cancellationToken = default)
        where TEvent : WebhookBaseEvent
    {
        var publishedEvent = new PublishedEvent<TEvent>(@event, headers);

        await Publish(publishedEvent, cancellationToken);
    }

    public Task Publish<TEvent>(PublishedEvent<TEvent> @event, CancellationToken cancellationToken = default)
        where TEvent : WebhookBaseEvent
    {
        var handlerType = lookup.FetchHandlerForEventType<TEvent>();

        if (handlerType is null)
        {
            return Task.CompletedTask;
        }

        var handler = (ITwitchWebhookEventHandler<TEvent>)provider.GetRequiredService(handlerType);

        return handler.Handle(@event, cancellationToken);
    }
}