using System;
using System.Reflection;
using twitchstreambot;
using twitchstreambot.infrastructure;
using twitchstreambot.infrastructure.DependencyInjection;

namespace twitchbot
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = RegisterTypes();

            TwitchAuthenticator authenticator = container.GetInstance<TwitchAuthenticator>();

            if (authenticator.Authenticate())
            {
                TwitchStreamBot bot = container.GetInstance<TwitchStreamBot>();

                if (bot.Start() != 0)
                {
                    Console.WriteLine("Error Status");
                    Console.WriteLine(bot.Error);
                }
            }
            else
            {
                Console.WriteLine("Bot is unable to authenticate with Twitch");
            }
        }

        private static IContainer RegisterTypes()
        {
            var container = new Container();

            container
                .When<TwitchAuthenticator>().Use(c => new TwitchAuthenticator("auth.json"))
                .When<TwitchConnection>().Use(c => new TwitchConnection
                {
                    BotName = "tbddotbot",
                    HostName = "irc.chat.twitch.tv",
                    Channel = "tbdgamer",
                    Port = 6667
                })
                .When<TwitchStreamBot>().Use(c =>
                {
                    var authenticator = c.GetInstance<TwitchAuthenticator>();
                    var commandFactory = c.GetInstance<CommandFactory>();
                    var connectionDetails = c.GetInstance<TwitchConnection>();

                    return new TwitchStreamBot(connectionDetails, authenticator.AuthenticationToken, commandFactory);
                })
                .When<CommandFactory>().Use(c =>
                {
                    var result = new CommandFactory(c);

                    result.LoadFromAssembly(Assembly.GetExecutingAssembly());

                    return result;
                });

            return container;
        }
    }
}