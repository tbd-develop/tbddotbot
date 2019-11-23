using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using twitchstreambot;
using twitchstreambot.api;
using twitchstreambot.pubsub;
using twitchstreambot.pubsub.Models;

namespace twitchbot
{
    public class BotService : IHostedService, IDisposable
    {
        private readonly TwitchStreamBot _bot;
        private readonly TwitchPubSub _pubSub;
        private readonly IConfiguration _configuration;
        private Task _botProcess;
        private Task _pubSubProcess;

        public BotService(TwitchStreamBot bot,
            TwitchPubSub pubSub,
            IConfiguration configuration)
        {
            _bot = bot;
            _pubSub = pubSub;
            _configuration = configuration;

            _bot.OnBotConnected += _bot_OnBotConnected;
            _pubSub.OnWhisperReceived += PubSubOnOnWhisperReceived;
            _pubSub.OnSubscriptionError += PubSubOnOnSubscriptionError;
        }

        private void PubSubOnOnSubscriptionError(string message)
        {
            Console.WriteLine(message);
        }

        private void PubSubOnOnWhisperReceived(PubSubResponseMessage responsemessage)
        {
            Console.WriteLine(responsemessage.Data.Message);
        }

        private void _bot_OnBotConnected(TwitchStreamBot streamer)
        {
            //streamer.SendToStream("The Bot is Up and Running");
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _botProcess = _bot.Start(cancellationToken);

            _pubSubProcess = _pubSub.Listen(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _pubSub.Stop(cancellationToken);

            await _bot.Stop();

            Task.WaitAll(_botProcess, _pubSubProcess);
        }

        public void Dispose()
        {
            _botProcess?.Dispose();

            _pubSubProcess?.Dispose();
        }
    }
}

