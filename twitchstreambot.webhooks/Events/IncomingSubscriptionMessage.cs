using twitchstreambot.webhooks.Api.Response;
using twitchstreambot.webhooks.Infrastructure.Interim;

namespace twitchstreambot.webhooks.Events;

public class IncomingSubscriptionMessage
{
    public WebhookSubscriptionContent<BroadcasterOnlyCondition, SubscriptionTransportDefinition> Subscription
    {
        get;
        set;
    } = null!;
}