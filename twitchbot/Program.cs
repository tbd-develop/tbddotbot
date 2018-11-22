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
            Auth authentication;

            using (StreamReader reader = new StreamReader("auth.json"))
            {
                authentication = JsonConvert.DeserializeObject<Auth>(reader.ReadToEnd());
            }

            TwitchStreamBot bot = new TwitchStreamBot(new TwitchConnection
            {
                BotName = "tbddotbot",
                HostName = "irc.chat.twitch.tv",
                Channel = "tbdgamer",
                Port = 6667
            }, authentication);

            bot.RegisterCommand<Uptime>("uptime");
            bot.RegisterCommand<Christmas>("christmas");
            bot.RegisterCommand<EightBall>("8ball");
            bot.RegisterCommand<DiceRoller>("roll");

            if (bot.Start() != 0)
            {
                Console.WriteLine("Error Status");
                Console.WriteLine(bot.Error);
            }
        }
    }
}