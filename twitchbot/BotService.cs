using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using twitchstreambot;

namespace twitchbot
{
    public class BotService : IHostedService, IDisposable
    {
        private readonly TwitchStreamBot _bot;
        private Task _botProcess;

        public BotService(TwitchStreamBot bot,
            IConfiguration configuration)
        {
            _bot = bot;

            _bot.OnBotConnected += _bot_OnBotConnected;
            _bot.OnBotDisconnected += _bot_OnBotDisconnected;
        }

        private void _bot_OnBotDisconnected(TwitchStreamBot streamer)
        {
            streamer.SendToStream("Lost Connection...");
        }

        private void _bot_OnBotConnected(TwitchStreamBot streamer)
        {
            streamer.SendToStream("The Bot is Up and Running");
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _botProcess = _bot.Start(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _bot.Stop();

            Task.WaitAll(_botProcess);
        }

        public void Dispose()
        {
            _botProcess?.Dispose();
        }
    }
}

