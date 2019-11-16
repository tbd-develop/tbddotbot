using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using twitchstreambot;
using twitchstreambot.basics;
using twitchstreambot.basics.Infrastructure;
using twitchstreambot.command;
using twitchstreambot.command.CommandDispatch;
using twitchstreambot.Infrastructure;
using twitchstreambot.Infrastructure.Configuration;
using twitchstreambot.Parsing;

namespace twitchbot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<BotService>()
                .Build();

            await CreateHostedService(configuration, args).Build().RunAsync();
        }

        private static IHostBuilder CreateHostedService(IConfiguration configuration, string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    var definedCommandsStore = new DefinedCommandsStore("definitions.json");

                    var commandSet = new CommandSet(new CommandRegistry[]
                        {new BasicsRegistry(services), new DefinedCommandsRegistry(services, definedCommandsStore)});

                    services.AddSingleton(configuration);

                    services.AddSingleton(c => new TwitchConnection
                    {
                        BotName = configuration["bot:name"],
                        HostName = configuration["bot:host"],
                        Channel = configuration["bot:channel"],
                        Port = int.Parse(configuration["bot:port"])
                    });

                    services.AddSingleton<IRCCommandHandler>();
                    services.AddSingleton(definedCommandsStore);

                    services.AddSingleton(c =>
                    {
                        var botConfiguration =
                            new TwitchBotBuilder()
                                .WithAuthentication(configuration["twitch:auth"])
                                .WithConnection(c.GetService<TwitchConnection>())
                                .WithCommands(b => { b.AddHandler<IRCCommandHandler>(IRCMessageType.PRIVMSG); })
                                .Build();

                        return new TwitchStreamBot(botConfiguration, c);
                    });

                    services.AddSingleton(c => new CommandDispatcher(commandSet, c));

                    services.AddHostedService<BotService>();
                });
        }
    }
}