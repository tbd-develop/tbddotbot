using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using twitchstreambot;
using twitchstreambot.pubsub;

namespace twitchbot
{
    public class BotService : IHostedService, IDisposable
    {
        private readonly TwitchStreamBot _bot;
        private Task _botProcess;
        private Task _pubSubProcess;
        private readonly TwitchPubSub _pubSub;

        public BotService(TwitchStreamBot bot,
            IConfiguration configuration,
            TwitchPubSub pubSub)
        {
            _bot = bot;
            _pubSub = pubSub;

            _bot.OnBotConnected += _bot_OnBotConnected;
            _bot.OnBotDisconnected += _bot_OnBotDisconnected;

            _pubSub.OnPubSubConnected += _pubSub_Connected;
            _pubSub.OnSubscriptionError += message => { Console.WriteLine($"Error {message}"); };
        }

        protected void _pubSub_Connected(CancellationToken cancellationToken)
        {
            _pubSubProcess = Task.Run(() => _pubSub.Listen(cancellationToken), cancellationToken);
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

            await _pubSub.Connect(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _bot.Stop();

            await _pubSub.Stop(cancellationToken);

            Task.WaitAll(_botProcess, _pubSubProcess);
        }

        public void Dispose()
        {
            _botProcess?.Dispose();
        }
    }
}

