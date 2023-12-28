using twitchstreambot.webhooks.Events.Values;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Drop.Entitlement;

[WebhookEvent("drop.entitlement.grant")]
public class Grant : WebhookBaseEvent
{
    public string Id { get; set; } = null!;

    public GrantData Data { get; set; } = null!;
}