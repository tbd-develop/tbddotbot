using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace twitchstreambot.api.Configuration
{
    public static class ApiConfiguration
    {
        public static void AddHelix(this IServiceCollection collection, IConfiguration configuration)
        {
            collection.AddHttpClient<TwitchHelix>((provider, client) =>
            {
                client.BaseAddress = new Uri("https://api.twitch.tv/");
                client.DefaultRequestHeaders.Add("Authorization",
                    $"Bearer {configuration["twitch:auth"]}");
            });

            collection.AddSingleton<TwitchHelix>();
        }

        public static void AddKraken(this IServiceCollection collection, IConfiguration configuration)
        {
            collection.AddHttpClient<TwitchKraken>((provider, client) =>
            {
                client.BaseAddress = new Uri("https://api.twitch.tv/");
                client.DefaultRequestHeaders.Add("Client-Id",
                    configuration["twitch:clientId"]);
                client.DefaultRequestHeaders.Add("Authorization",
                    $"OAuth {configuration["twitch:auth"]}");
                client.DefaultRequestHeaders.Add("Accept", "application/vnd.twitchtv.v5+json");
            });

            collection.AddSingleton<TwitchKraken>();
        }
    }
}