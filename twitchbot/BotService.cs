using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
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
        private readonly StreamGames _games;
        private Container _container;

        public BotService()
        {
            var container = RegisterTypes();

            TwitchAuthenticator authenticator = container.GetInstance<TwitchAuthenticator>();

            if (authenticator.Authenticate(false))
            {
                _bot = container.GetInstance<TwitchStreamBot>();

                _coordinator = new TimedMessagesCoordinator(_bot);

                _games = container.GetInstance<StreamGames>();
            }
            else
            {
                Console.WriteLine("Bot is unable to authenticate with Twitch");
            }
        }

        public void Start()
        {
            StartAsync().Wait();
        }

        public async Task StartAsync()
        {
            var client = await SignalRClient.Instance
                .Initialize("https://tbddotbot.azurewebsites.net/messages")
                .Start();

            client.OnBotMessageReceived += BotMessageReceived;

            _coordinator.Start();

            if (_bot.Start() != 0)
            {
                Console.WriteLine("Error Status");
                Console.WriteLine(_bot.Error);
            }
        }

        private void BotMessageReceived(SignalRClient client, object args)
        {
            if (args is ChannelNotification notification)
            {
                if (notification.Data != null)
                {
                    _games.MergeResults("hangman", notification.Data);

                    var sorted = string.Join(", ", notification.Data.OrderByDescending(d => d.Value)
                        .Select(s => $"{s.Key} had {s.Value}").ToArray());

                    _bot.SendToStream(sorted);
                }

                _bot.SendToStream(notification.Message);
            }
        }

        public void Stop()
        {
            StopAsync().Wait();
        }

        public async Task StopAsync()
        {
            _coordinator.Stop();

            await SignalRClient.Instance.Stop();

            _bot.Stop();
        }

        private IContainer RegisterTypes()
        {
            _container = new Container();

            _container
                .When<StreamGames>().Use<StreamGames>()
                .When<TwitchAuthenticator>().AsSingleton().Use(c => new TwitchAuthenticator("auth.json"))
                .When<TwitchConnection>().AsSingleton().Use(c => new TwitchConnection
                {
                    BotName = "tbddotbot",
                    HostName = "irc.chat.twitch.tv",
                    Channel = "tbdgamer",
                    Port = 6667
                })
                .When<IStorage>().Use<MongoDbStore>()
                .When<TwitchStreamBot>().AsSingleton().Use(c =>
                {
                    var authenticator = c.GetInstance<TwitchAuthenticator>();
                    var commandFactory = c.GetInstance<CommandFactory>();
                    var connectionDetails = c.GetInstance<TwitchConnection>();

                    var bot = new TwitchStreamBot(connectionDetails, authenticator.AuthenticationToken, commandFactory);

                    bot.OnCommandReceived += TwitchCommandReceived;

                    return bot;
                })
                .When<CommandFactory>().AsSingleton().Use(c =>
                {
                    var result = new CommandFactory(c);

                    result.LoadFromAssembly(Assembly.GetExecutingAssembly());

                    return result;
                })
                .When<SignalRClient>().AsSingleton().Use(c => SignalRClient.Instance);

            return _container;
        }

        private void TwitchCommandReceived(TwitchStreamBot sender, CommandArgs args)
        {
            //if (args.Message.IsBotCommand)
            //{
            //    Console.WriteLine($"Message sent By {args.Message.User.Name}");

            //    foreach (var header in args.Message.Headers)
            //    {
            //        Console.WriteLine($"{header.Key} = {header.Value}");
            //    }
            //}
        }
    }
}

