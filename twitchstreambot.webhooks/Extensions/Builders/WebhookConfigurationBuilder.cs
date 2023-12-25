using Microsoft.Extensions.DependencyInjection;
using twitchstreambot.webhooks.Infrastructure;
using twitchstreambot.webhooks.Infrastructure.Contracts;

namespace twitchstreambot.webhooks.Extensions.Builders;

public static class Delegates
{
    public static SecretProviderDelegate DefaultSecretProviderDelegate =>
        (provider, headers, request) => provider.SecretForSubscriptionType(headers.SubscriptionType!);
}

public class WebhookConfigurationBuilder(IServiceCollection services)
{
    public WebhookConfigurationBuilder AddSecretProvider<TProvider>(SecretProviderDelegate? @delegate = null)
        where TProvider : class, ISecretProvider
    {
        services.AddScoped<ISecretProvider, TProvider>();
        services.AddScoped<SecretProviderDelegate>(_ => @delegate ?? Delegates.DefaultSecretProviderDelegate);
        services.AddScoped<GetSecretDelegate>(provider => (headers, request) =>
            provider.GetRequiredService<SecretProviderDelegate>()(
                provider.GetRequiredService<ISecretProvider>(),
                headers,
                request));

        return this;
    }

    public WebhookConfigurationBuilder AddEventDispatcher<TDispatcher>()
        where TDispatcher : class, IWebhookEventDispatcher
    {
        services.AddScoped<IWebhookEventDispatcher, TDispatcher>();

        return this;
    }
}