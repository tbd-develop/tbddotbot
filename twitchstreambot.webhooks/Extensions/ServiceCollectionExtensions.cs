using Microsoft.Extensions.DependencyInjection;
using twitchstreambot.webhooks.Extensions.Builders;
using twitchstreambot.webhooks.Infrastructure;

namespace twitchstreambot.webhooks.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebhooks(this IServiceCollection services,
        Action<WebhookConfigurationBuilder> configure)
    {
        services.AddSingleton<EventMapper>();

        var builder = new WebhookConfigurationBuilder(services);

        configure(builder);

        return services;
    }
}