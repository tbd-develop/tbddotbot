using twitchstreambot.Api.Requests.WebHook;

namespace twitchstreambot.webhooks.Models;

public class IncomingSubscriptionMessage
{
    public WebhookSubscriptionContent<BroadcasterOnlyCondition, SubscriptionTransportDefinition> Subscription
    {
        get;
        set;
    } = null!;
}