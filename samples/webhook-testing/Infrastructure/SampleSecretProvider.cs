using twitchstreambot.webhooks.Infrastructure;
using twitchstreambot.webhooks.Infrastructure.Contracts;

namespace webhook_testing.Infrastructure;

public class SampleSecretProvider : ISecretProvider
{
    private readonly string _secret = "this-is-a-secret";

    public string SecretForSubscriptionType(string subscriptionType)
    {
        return _secret;
    }

    public string SecretForSubscriptionType(string subscriptionType, string userIdentifier)
    {
        throw new NotImplementedException();
    }
}