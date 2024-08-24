using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using twitchbot.Commands;
using twitchbot.Infrastructure;
using twitchbot.Middleware;
using twitchstreambot.Infrastructure.Extensions;

namespace twitchbot;

class Program
{
    static async Task Main(string[] args)
    {
        await CreateHostedService(args).Build().RunAsync();
    }

    private static IHostBuilder CreateHostedService(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((_, builder) =>
            {
                builder
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .AddUserSecrets<BotService>();
            })
            .ConfigureServices((_, services) =>
            {
                services.AddSingleton<GameState>();

                services.AddTwitch(configure =>
                {
                    configure.AddIrcBot(config =>
                    {
                        config.AddCommands(typeof(HelloWorldCommand).Assembly)
                            .AddMessagingMiddleware<GameMiddleware>();
                    });
                    configure.AddTwitchApis();
                });

                services.AddHostedService<BotService>();
            });
    }
}