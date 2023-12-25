namespace twitchstreambot.Api.Requests.WebHook;

public abstract class SubscriptionParameters(
    string typeName,
    string version,
    SubscriptionCondition condition,
    SubscriptionTransportDefinition transport)
{
    public string TypeName { get; } = typeName;
    public string Version { get; } = version;
    public SubscriptionCondition Condition { get; } = condition;
    public SubscriptionTransportDefinition Transport { get; } = transport;
}