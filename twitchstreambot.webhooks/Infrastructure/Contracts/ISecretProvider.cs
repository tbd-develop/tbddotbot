namespace twitchstreambot.webhooks.Infrastructure.Contracts;

public interface ISecretProvider
{
    string SecretForSubscriptionType(string subscriptionType);
    string SecretForSubscriptionType(string subscriptionType, string userIdentifier);
}