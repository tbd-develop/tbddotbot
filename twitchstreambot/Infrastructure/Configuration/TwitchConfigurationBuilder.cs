using System;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using twitchstreambot.Api;
using twitchstreambot.Dispatch;
using twitchstreambot.Infrastructure.Delegates;

namespace twitchstreambot.Infrastructure.Configuration;

public class TwitchConfigurationBuilder(IServiceCollection services)
{
    /// <summary>
    /// Add Irc bot with dispatch messaging
    /// </summary>
    /// <param name="configure">Configuration Action</param>
    public void AddIrcBot(Action<TwitchBotConfigurationBuilder> configure)
    {
        services.AddSingleton<IMessageDispatcher, DefaultMessageDispatcher>();
        services.AddSingleton<TwitchStreamBot>();
        services.AddSingleton<IStreamOutput>(provider => provider.GetRequiredService<TwitchStreamBot>());

        services.AddSingleton(provider =>
        {
            var twitchConnection = new TwitchConnection();

            var configuration = provider.GetRequiredService<IConfiguration>();

            configuration.GetSection("twitch:bot")
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

    public void AddTwitchApis()
    {
        services.AddHttpClient<TwitchApi>((_, client) => { client.BaseAddress = new Uri("https://id.twitch.tv"); });
        services.AddSingleton<CreateTwitchApiOptionsDelegate>(() => new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        });
        
        services.AddHttpClient<TwitchHelix>((provider, client) =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            
            client.BaseAddress = new Uri("https://api.twitch.tv/");
            client.DefaultRequestHeaders.Add("Client-Id",
                configuration["twitch:clientId"]);
            client.DefaultRequestHeaders.Add("Authorization",
                $"Bearer {configuration["twitch:authToken"]}");
        });
    }
}