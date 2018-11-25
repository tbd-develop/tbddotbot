using System;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using twitchbot.commands;
using twitchbot.infrastructure;
using twitchbot.models;

namespace twitchbot
{
    class Program
    {
        static void Main(string[] args)
        {
            TwitchAuthenticator authenticator = new TwitchAuthenticator("auth.json");

            if (authenticator.Authenticate())
            {
                TwitchStreamBot bot = new TwitchStreamBot(new TwitchConnection
                {
                    BotName = "tbddotbot",
                    HostName = "irc.chat.twitch.tv",
                    Channel = "tbdgamer",
                    Port = 6667
                }, authenticator.AuthenticationToken);

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
    }
}