using Microsoft.Extensions.DependencyInjection;
using twitchstreambot.webhooks.Extensions.Builders;
using twitchstreambot.webhooks.Publishing.Builders;
using twitchstreambot.webhooks.Publishing.Contracts;

namespace twitchstreambot.webhooks.Publishing;

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