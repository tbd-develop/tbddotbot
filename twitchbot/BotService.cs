﻿using System;
using System.Reflection;
using twitchstreambot;
using twitchstreambot.infrastructure;
using twitchstreambot.infrastructure.DependencyInjection;

namespace twitchbot
{
    public class BotService
    {
        private readonly TwitchStreamBot _bot;
        private Container _container;

        public BotService()
        {
            var container = RegisterTypes();

            TwitchAuthenticator authenticator = container.GetInstance<TwitchAuthenticator>();

            if (authenticator.Authenticate(false))
            {
                _bot = container.GetInstance<TwitchStreamBot>();
            }
            else
            {
                Console.WriteLine("Bot is unable to authenticate with Twitch");
            }
        }

        public void Start()
        {
            if (_bot.Start() != 0)
            {
                Console.WriteLine("Error Status");
                Console.WriteLine(_bot.Error);
            }
        }

        public void Stop()
        {
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

            return _container;
        }
    }
}