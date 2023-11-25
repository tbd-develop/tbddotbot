using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using twitchstreambot.Infrastructure.Attributes;

namespace twitchstreambot.Infrastructure.Extensions;

public static class CommandLoaderExtensions
{
    public static void AddCommands(this IServiceCollection serviceCollection, params Assembly[] assemblies)
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