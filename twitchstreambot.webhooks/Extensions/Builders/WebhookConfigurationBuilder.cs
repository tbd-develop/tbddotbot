using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using twitchstreambot.webhooks.Infrastructure.Contracts;
using twitchstreambot.webhooks.Infrastructure.Delegates;

namespace twitchstreambot.webhooks.Extensions.Builders;

public class WebhookConfigurationBuilder(IServiceCollection services)
{
    public IServiceCollection Services => services;

    public void Initialize()
    {
        services.AddSingleton<CreateTwitchWebhookOptionsDelegate>((_) =>
        {
            return () => new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
        });
    }

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
}