using System.Reflection;
using twitchstreambot.webhooks.Events;
using twitchstreambot.webhooks.Infrastructure.Attributes;
using twitchstreambot.webhooks.Infrastructure.Interim;

namespace twitchstreambot.webhooks.Api.Parameters;

public class WebhookSubscriptionParameters<TSubscriptionEvent> :
    SubscriptionParameters<BroadcasterOnlyCondition>
    where TSubscriptionEvent : WebhookBaseEvent
{
    public WebhookSubscriptionParameters(
        BroadcasterOnlyCondition condition,
        SubscriptionTransportDefinition transport) : base(condition, transport)
    {
        var attribute = typeof(TSubscriptionEvent).GetCustomAttribute<WebhookEventAttribute>();

        if (attribute is null) return;
        
        Type = attribute.Event;
        Version = $"{attribute.Version}";
    }
}