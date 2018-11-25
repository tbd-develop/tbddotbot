using System;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using twitchbot.commands;
using twitchbot.infrastructure;
using twitchbot.infrastructure.DependencyInjection;
using twitchbot.models;

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
                .When<Christmas>().Use<Christmas>()
                .When<DiceRoller>().Use<DiceRoller>()
                .When<EightBall>().Use<EightBall>()
                .When<Uptime>().Use<Uptime>()
                .When<CommandFactory>().Use<CommandFactory>();

            return container;
        }
    }
}