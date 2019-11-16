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
            HostFactory.Run(cfg =>
            {
                cfg.Service<BotService>(svc =>
                {
=======
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
                    var definedCommandsStore = new DefinedCommandsStore("definitions.json");

                    var commandSet = new CommandSet(new CommandRegistry[]
                        {new BasicsRegistry(services), new DefinedCommandsRegistry(services, definedCommandsStore)});

                    services.AddSingleton(c => new TwitchConnection
                    {
                        BotName = context.Configuration["bot:name"],
                        HostName = context.Configuration["bot:host"],
                        Channel = context.Configuration["bot:channel"],
                        Port = int.Parse(context.Configuration["bot:port"])
                    });

                    services.AddSingleton<IRCCommandHandler>();
                    services.AddSingleton(definedCommandsStore);

                    services.AddSingleton(c =>
                    {
                        var botConfiguration =
                            new TwitchBotBuilder()
                                .WithAuthentication(context.Configuration["twitch:auth"])
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