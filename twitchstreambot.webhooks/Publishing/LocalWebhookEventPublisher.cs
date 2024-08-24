using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using twitchstreambot.webhooks.Events;
using twitchstreambot.webhooks.Infrastructure;
using twitchstreambot.webhooks.Publishing.Contracts;

namespace twitchstreambot.webhooks.Publishing;

public class LocalWebhookEventPublisher(
    ILocalEventLookup lookup,
    IServiceProvider provider) : IWebhookEventPublisher
{
    public Task Publish(WebhookBaseEvent @event, TwitchHeaderCollection headers,
        CancellationToken cancellationToken = default)
    {
        var eventType = @event.GetType();
        var bindingAttributes = BindingFlags.NonPublic | BindingFlags.Instance;

        var method = typeof(LocalWebhookEventPublisher)
            .GetMethod(nameof(BuildAndPublish), bindingAttributes)!;

        var generic = method.MakeGenericMethod(eventType);

        return (Task)generic.Invoke(this, new object[] { @event, headers, cancellationToken })!;
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
    
    private async Task BuildAndPublish<TEvent>(TEvent @event, TwitchHeaderCollection headers,
        CancellationToken cancellationToken = default)
        where TEvent : WebhookBaseEvent
    {
        var publishedEvent = new PublishedEvent<TEvent>(@event, headers);

        await Publish(publishedEvent, cancellationToken);
    }
}