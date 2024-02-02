namespace twitchstreambot.webhooks.Extensions.Builders;

public static class Delegates
{
    public static SecretProviderDelegate DefaultSecretProviderDelegate =>
        (provider, headers, _) => provider.SecretForSubscriptionType(headers.SubscriptionType!);
}