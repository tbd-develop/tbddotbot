using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using twitchstreambot.Dispatch;
using twitchstreambot.Infrastructure.Attributes;
using twitchstreambot.Infrastructure.Configuration;

namespace twitchstreambot.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddTwitchStreamBot(this IServiceCollection services,
        Action<TwitchBotConfigurationBuilder> configure)
    {
        services.AddSingleton<IMessageDispatcher, DefaultMessageDispatcher>();
        services.AddSingleton<TwitchStreamBot>();
        services.AddSingleton<IStreamOutput>(provider => provider.GetRequiredService<TwitchStreamBot>());

        services.AddSingleton(provider =>
        {
            var twitchConnection = new TwitchConnection();

            var configuration = provider.GetRequiredService<IConfiguration>();

            configuration.GetSection("bot")
                .Bind(twitchConnection);

            return twitchConnection;
        });

        services.AddSingleton(provider =>
        {
            var twitchBotConfiguration = new TwitchBotConfiguration();

            var configuration = provider.GetRequiredService<IConfiguration>();

            configuration
                .GetSection("twitch").Bind(twitchBotConfiguration);

            return twitchBotConfiguration;
        });

        var builder = new TwitchBotConfigurationBuilder(services);

        configure(builder);

        builder.ConstructMiddlewarePipeline();
    }

    private static void AddCommands(this IServiceCollection serviceCollection, params Assembly[] assemblies)
    {
        Dictionary<string, Type> availableCommands = new();

        foreach (var assembly in assemblies)
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

                serviceCollection.AddTransient(command.Type);
            }
        }

        serviceCollection.AddSingleton<ICommandLookup>(new DefaultCommandLookup(availableCommands));
    }
}