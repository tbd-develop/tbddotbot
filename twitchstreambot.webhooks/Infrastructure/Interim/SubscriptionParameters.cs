using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Events;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Infrastructure.Interim;

public abstract class SubscriptionParameters<
    TSubscriptionCondition>(
    TSubscriptionCondition condition,
    SubscriptionTransportDefinition transport)
    where TSubscriptionCondition : SubscriptionCondition
{
    public string Type { get; protected set; } = null!;
    public string Version { get; protected set; } = null!;
    public TSubscriptionCondition Condition { get; } = condition;
    public SubscriptionTransportDefinition Transport { get; } = transport;
}