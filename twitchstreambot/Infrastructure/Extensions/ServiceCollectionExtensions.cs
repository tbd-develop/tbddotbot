using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using twitchstreambot.Dispatch;
using twitchstreambot.Infrastructure.Configuration;

namespace twitchstreambot.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddTwitchStreamBot(this IServiceCollection services, params Assembly[] commandAssemblies)
    {
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


        services.AddSingleton<IMessageDispatcher, DefaultMessageDispatcher>();
        services.AddSingleton<TwitchStreamBot>();
        services.AddCommands(commandAssemblies);
    }
}