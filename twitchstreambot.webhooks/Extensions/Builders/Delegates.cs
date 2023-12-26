namespace twitchstreambot.webhooks.Extensions.Builders;

public static class Delegates
{
    public static SecretProviderDelegate DefaultSecretProviderDelegate =>
        (provider, headers, request) => provider.SecretForSubscriptionType(headers.SubscriptionType!);
}