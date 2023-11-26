using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using twitchstreambot.Infrastructure;
using twitchstreambot.Infrastructure.Communications;
using twitchstreambot.Infrastructure.Configuration;

namespace twitchstreambot
{
    public class TwitchStreamBot
    {
        private readonly TwitchBotConfiguration _configuration;
        private readonly IMessageDispatcher _dispatcher;
        private readonly TwitchConnection _connection;
        private ChannelReader? _channelReader;
        private ChannelWriter? _channelWriter;

        public delegate void BotConnectedHandler(TwitchStreamBot streamer);

        public event BotConnectedHandler OnBotConnected;
        public event BotConnectedHandler OnBotDisconnected;

        public TwitchStreamBot(
            TwitchConnection connection,
            TwitchBotConfiguration configuration,
            IMessageDispatcher dispatcher)
        {
            _connection = connection;
            _configuration = configuration;
            _dispatcher = dispatcher;
        }

        public async Task Start(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    using var client = new TcpClient(_connection.HostName, _connection.Port);
                    
                    await using var stream = client.GetStream();

                    _channelReader = new ChannelReader(stream);
                    _channelWriter = new ChannelWriter(stream)
                    {
                        Channel = _connection.Channel,
                        BotName = _connection.Name,
                        AuthToken = _configuration.AuthToken
                    };

                    _channelReader.OnMessageReceived += ReaderOnMessageReceived;

                    _channelWriter.Authenticate();

                    SendTwitchCommand("CAP REQ :twitch.tv/membership");
                    SendTwitchCommand("CAP REQ :twitch.tv/tags twitch.tv/commands");

                    OnBotConnected?.Invoke(this);

                    await _channelReader.ListenForMessages(cancellationToken);

                    OnBotDisconnected?.Invoke(this);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);

                    OnBotDisconnected?.Invoke(this);
                }
            }
        }

        public async Task Stop()
        {
            if (_channelReader is not null)
            {
                _channelReader!.SignalShutdown();

                _channelReader!.Dispose();

                _channelReader = null;
            }

            if (_channelWriter is not null)
            {
                await _channelWriter.FlushAsync();

                await _channelWriter.DisposeAsync();

                _channelWriter = null;
            }
        }

        private void ReaderOnMessageReceived(ChannelReader sender, MessageReceivedArgs args)
        {
            if (args.Message.StartsWith("PING"))
            {
                SendTwitchCommand("PONG", false);

                return;
            }

            _dispatcher.Dispatch(args.Message);
        }

        public void SendToStream(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                _channelWriter.SendMessage(message);
            }
        }

        public void SendTwitchCommand(string command, bool awaitResponse = true)
        {
            _channelWriter.SendCommand(command);

            if (awaitResponse)
            {
                _channelReader.ReadLine();
            }
        }
    }
}