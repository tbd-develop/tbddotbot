using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using twitchstreambot.Infrastructure.Extensions;
using twitchstreambot.Middleware;

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
            .ConfigureAppConfiguration((context, builder) =>
            {
                builder
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .AddUserSecrets<BotService>();
            })
            .ConfigureServices((context, services) =>
            {
                services.AddTwitchStreamBot(configure =>
                {
                    // configure.AddCommands(typeof().Assembly);
#if DEBUG
                    configure.AddMessagingMiddleware<ConsoleOutMiddleware>();
#endif
                    configure.AddMessagingMiddleware<CommandMiddleware>();
                });

                services.AddHelix(context.Configuration);
                services.AddTwitchApi(context.Configuration);

                services.AddHostedService<BotService>();
            });
    }
}