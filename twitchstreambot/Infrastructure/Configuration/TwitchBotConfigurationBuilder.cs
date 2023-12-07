using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using twitchstreambot.Dispatch;
using twitchstreambot.Infrastructure.Attributes;

namespace twitchstreambot.Infrastructure.Configuration;

public class TwitchBotConfigurationBuilder
{
    private readonly IServiceCollection _serviceCollection;
    private readonly List<Type> _middlewares = new();

    public TwitchBotConfigurationBuilder(IServiceCollection serviceCollection)
    {
        _serviceCollection = serviceCollection;
    }

    public TwitchBotConfigurationBuilder AddCommands(params Assembly[] commandAssemblies)
    {
        Dictionary<string, Type> availableCommands = new();

        foreach (var assembly in commandAssemblies)
        {
            var assemblyCommandSet = from t in assembly.GetTypes()
                let attribute = t.GetCustomAttribute<TwitchCommandAttribute>()
                where attribute != null && !string.IsNullOrEmpty(attribute.Action) && !attribute.Ignore
                select new
                {
                    Action = attribute.Action.ToLower(),
                    Type = t
                };

            foreach (var command in assemblyCommandSet)
            {
                availableCommands.Add(command.Action, command.Type);

                _serviceCollection.AddTransient(command.Type);
            }
        }

        _serviceCollection.AddSingleton<ICommandLookup>(new DefaultCommandLookup(availableCommands));

        return this;
    }

    public TwitchBotConfigurationBuilder AddMessagingMiddleware<TMiddleware>()
        where TMiddleware : class, IMessagingMiddleware
    {
        _serviceCollection.AddSingleton<TMiddleware>();

        _middlewares.Add(typeof(TMiddleware));

        return this;
    }

    public void ConstructMiddlewarePipeline()
    {
        _serviceCollection.AddSingleton<IMessagingPipeline>(provider =>
        {
            var middlewares = _middlewares
                .Select(provider.GetRequiredService)
                .OrderBy(middleware => _middlewares.IndexOf(middleware.GetType()))
                .OfType<IMessagingMiddleware>();

            return new MessagingPipeline(middlewares);
        });
    }
}