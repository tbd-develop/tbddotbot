using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using twitchstreambot;

namespace twitchbot
{
    public class BotService : IHostedService
    {
        private readonly TwitchStreamBot _bot;

        public BotService(TwitchStreamBot bot)
        {
            _bot = bot;

            _bot.OnBotConnected += _bot_OnBotConnected;
        }

        private void _bot_OnBotConnected(TwitchStreamBot streamer)
        {
            streamer.SendToStream("The Bot is Up and Running");
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (await _bot.Start() != 0)
            {
                Console.WriteLine("Unable to start Bot");
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _bot.Stop();
        }
    }
}

