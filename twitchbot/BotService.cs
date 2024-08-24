using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using twitchstreambot;
using twitchstreambot.Api;

namespace twitchbot
{
    public class BotService : IHostedService, IDisposable
    {
        private readonly TwitchStreamBot _bot;
        private readonly TwitchApi _api;
        private Task _botProcess = null!;
        private Task _pubSubProcess = null!;

        public BotService(TwitchStreamBot bot, TwitchApi api)
        {
            _bot = bot;
            _api = api;

            _bot.OnBotConnected += _bot_OnBotConnected;
            _bot.OnBotDisconnected += _bot_OnBotDisconnected;
        }

        private void _bot_OnBotDisconnected(TwitchStreamBot streamer)
        {
            streamer.SendToStream("Lost Connection...");
        }

        private void _bot_OnBotConnected(TwitchStreamBot streamer)
        {
            //streamer.SendToStream("The Bot is Up and Running");
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var result = await _api.Validate();

            _botProcess = _bot.Start(cancellationToken);

            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _bot.Stop();

            Task.WaitAll(new[]
            {
                _botProcess,
                _pubSubProcess
            }, cancellationToken);
        }

        public void Dispose()
        {
            _botProcess?.Dispose();
        }
    }
}