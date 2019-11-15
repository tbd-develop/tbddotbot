using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using twitchbot.Infrastructure;
using twitchstreambot;
using twitchstreambot.basics;
using twitchstreambot.command;
using twitchstreambot.command.CommandDispatch;
using twitchstreambot.Infrastructure;
using twitchstreambot.Infrastructure.Configuration;
using twitchstreambot.Infrastructure.DependencyInjection;
using twitchstreambot.Parsing;

namespace twitchbot
{
    public class BotService
    {
        private readonly TwitchStreamBot _bot;
        private readonly TimedMessagesCoordinator _coordinator;
        private Container _container;

        public BotService()
        {
            var container = RegisterTypes();

            _bot = container.GetInstance<TwitchStreamBot>();

            _bot.OnBotConnected += _bot_OnBotConnected;

            _coordinator = new TimedMessagesCoordinator(_bot);
        }

        private void _bot_OnBotConnected(TwitchStreamBot streamer)
        {
            streamer.SendToStream("tbdDOTbot is up and ready to rumble");
        }

        public void Start()
        {
            StartAsync().Wait();
        }

        public async Task StartAsync()
        {
            _coordinator.Start();

            if (_bot.Start() != 0)
            {
                Console.WriteLine("Error Status");
            }
        }

        public void Stop()
        {
            StopAsync().Wait();
        }

        public async Task StopAsync()
        {
            _coordinator.Stop();

            _bot.Stop();
        }

        private IContainer RegisterTypes()
        {
            _container = new Container();

            _container
                .When<IConfiguration>().AsSingleton().Use(c => new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .AddUserSecrets<BotService>()
                    .Build())
                .When<TwitchConnection>().AsSingleton().Use(c => new TwitchConnection
                {
                    BotName = "tbddotbot",
                    HostName = "irc.chat.twitch.tv",
                    Channel = "tbdgamer",
                    Port = 6667
                })
                .When<TwitchStreamBot>().AsSingleton().Use(c =>
                {
                    var configuration =
                        new TwitchBotBuilder()
                            .WithAuthentication(c.GetInstance<IConfiguration>()["twitch:auth"])
                            .WithConnection(c.GetInstance<TwitchConnection>())
                            .WithCommands(b =>
                            {
                                b.AddHandler<IRCCommandHandler>(IRCMessageType.PRIVMSG);
                            })
                            .Build();

                    return new TwitchStreamBot(configuration, c);
                })
                .When<CommandDispatcher>().AsSingleton().Use(c =>
                {
                    return new CommandDispatcher(
                        new CommandSet(new ICommandRegistry[]
                            {new BasicsRegistry(), new DefinedCommandsRegistry()}), c);
                })
                .When<SignalRClient>().AsSingleton().Use(c => SignalRClient.Instance);

            return _container;
        }
    }
}

