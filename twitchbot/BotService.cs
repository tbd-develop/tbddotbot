using System;
using System.IO;
using System.Reflection;
using twitchbot.Infrastructure;
using twitchstreambot;
using twitchstreambot.infrastructure;
using twitchstreambot.infrastructure.DependencyInjection;

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

            TwitchAuthenticator authenticator = container.GetInstance<TwitchAuthenticator>();

            if (authenticator.Authenticate(false))
            {
                _bot = container.GetInstance<TwitchStreamBot>();

                _coordinator = new TimedMessagesCoordinator(_bot);
            }
            else
            {
                Console.WriteLine("Bot is unable to authenticate with Twitch");
            }
        }

        public void Start()
        {
            _coordinator.Start();

            if (_bot.Start() != 0)
            {
                Console.WriteLine("Error Status");
                Console.WriteLine(_bot.Error);
            }
        }

        public void Stop()
        {
            _coordinator.Stop();
            _bot.Stop();
        }

        private IContainer RegisterTypes()
        {
            _container = new Container();

            _container
                .When<TwitchAuthenticator>().Use(c => new TwitchAuthenticator("auth.json"))
                .When<TwitchConnection>().Use(c => new TwitchConnection
                {
                    BotName = "tbddotbot",
                    HostName = "irc.chat.twitch.tv",
                    Channel = "tbdgamer",
                    Port = 6667
                })
                .When<IStorage>().Use(c => new LiteDbStore(Path.Combine(AppContext.BaseDirectory, "channel.db")))
                .When<TwitchStreamBot>().Use(c =>
                {
                    var authenticator = c.GetInstance<TwitchAuthenticator>();
                    var commandFactory = c.GetInstance<CommandFactory>();
                    var connectionDetails = c.GetInstance<TwitchConnection>();

                    var bot = new TwitchStreamBot(connectionDetails, authenticator.AuthenticationToken, commandFactory);

                    bot.OnCommandReceived += TwitchCommandReceived;

                    return bot;
                })
                .When<CommandFactory>().Use(c =>
                {
                    var result = new CommandFactory(c);

                    result.LoadFromAssembly(Assembly.GetExecutingAssembly());

                    return result;
                });

            return _container;
        }

        private void TwitchCommandReceived(TwitchStreamBot sender, CommandArgs args)
        {

        }
    }
}

