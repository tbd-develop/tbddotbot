using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using twitchstreambot;
using twitchstreambot.api.Configuration;
using twitchstreambot.basics;
using twitchstreambot.command;
using twitchstreambot.command.CommandDispatch;
using twitchstreambot.Infrastructure;
using twitchstreambot.Infrastructure.Configuration;
using twitchstreambot.Parsing;
using twitchstreambot.pubsub;

namespace twitchbot
{
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
                    services.AddSingleton(c => new TwitchConnection
                    {
                        BotName = context.Configuration["bot:name"],
                        HostName = context.Configuration["bot:host"],
                        Channel = context.Configuration["bot:channel"],
                        Port = int.Parse(context.Configuration["bot:port"])
                    });

                    services.AddSingleton<BotCommandsHandler>();

                    services.AddSingleton(c =>
                    {
                        var botConfiguration =
                            new TwitchBotBuilder()
                                .WithAuthentication(context.Configuration["twitch:auth"])
                                .WithConnection(c.GetService<TwitchConnection>())
                                .WithHandler<BotCommandsHandler>(IRCMessageType.PRIVMSG)
                                .Build();

                        return new TwitchStreamBot(botConfiguration, c);
                    });

                    // Configure your command set 
                    services.AddSingleton<ICommandSet, BasicCommandSet>();

                    // If you have created your own commandsets, you can use the merged command set
                    // services.AddSingleton<ICommandSet>(provider => new MergedCommandSet(
                    //    provider.GetService<BasicCommandSet>()));

                    // Configure the dispatcher
                    services.AddSingleton<ICommandDispatcher, CommandDispatcher>();

                    services.AddHelix(context.Configuration);
                    services.AddKraken(context.Configuration);
                    services.AddTwitchAPI(context.Configuration);

                    services.AddSingleton<TwitchPubSub>();

                    services.AddHostedService<BotService>();
                });
        }
    }
}