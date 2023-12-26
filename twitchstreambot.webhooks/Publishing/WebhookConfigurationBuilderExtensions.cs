using Microsoft.Extensions.DependencyInjection;
using twitchstreambot.webhooks.Extensions.Builders;
using twitchstreambot.webhooks.Infrastructure.Builders;

namespace twitchstreambot.webhooks.Infrastructure;

public static class WebhookConfigurationBuilderExtensions
{
    public static WebhookConfigurationBuilder AddLocalEventHandling(this WebhookConfigurationBuilder parent,
        Action<LocalPublisherBuilder> configure)
    {
        parent.Services.AddSingleton<IEventPublisher, LocalEventPublisher>();

        var builder = new LocalPublisherBuilder(parent.Services);

        configure(builder);

        return parent;
    }
}