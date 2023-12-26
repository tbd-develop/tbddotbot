using System.Diagnostics;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using twitchstreambot.webhooks.Events;

namespace twitchstreambot.webhooks.Infrastructure.Builders;

public class LocalPublisherBuilder(IServiceCollection services)
{
    public LocalPublisherBuilder AddHandlersFromAssembly(Assembly assembly)
    {
        var lookups = (from type in assembly.GetTypes()
            where type.IsClass && !type.IsAbstract
            let matching = type.GetInterfaces().SingleOrDefault(t =>
                t.IsGenericType && t.GetGenericTypeDefinition() == typeof(ITwitchWebhookEventHandler<>))
            where matching is not null
            select new
            {
                EventType = matching.GetGenericArguments().Single(),
                HandlerType = type
            }).ToDictionary(x => x.EventType, x => x.HandlerType);

        foreach (var lookup in lookups)
        {
            services.AddTransient(lookup.Value);
        }

        services.AddSingleton<ILocalEventLookup>(provider => new LocalEventLookup(lookups));

        return this;
    }
}