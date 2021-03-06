﻿using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using twitchstreambot.Infrastructure;
using twitchstreambot.Infrastructure.Communications;
using twitchstreambot.Infrastructure.Configuration;
using twitchstreambot.Parsing;

namespace twitchstreambot
{
    public class TwitchStreamBot
    {
        private readonly TwitchBotConfiguration _configuration;
        private readonly IServiceProvider _container;
        private ChannelReader _channelReader;
        private ChannelWriter _channelWriter;

        public string User => _configuration.Connection.BotName;
        public string Channel => _configuration.Connection.Channel;

        public delegate void BotConnectedHandler(TwitchStreamBot streamer);
        public event BotConnectedHandler OnBotConnected;
        public event BotConnectedHandler OnBotDisconnected;

        public TwitchStreamBot(TwitchBotConfiguration configuration, IServiceProvider container)
        {
            _configuration = configuration;
            _container = container;
        }

        public async Task Start(CancellationToken cancellationToken)
        {
            var connection = _configuration.Connection;

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    using (var client = new TcpClient(connection.HostName, connection.Port))
                    {
                        using (var stream = client.GetStream())
                        using (_channelReader = new ChannelReader(stream))
                        using (_channelWriter = new ChannelWriter(stream)
                        {
                            Channel = connection.Channel,
                            BotName = connection.BotName,
                            AuthToken = _configuration.AuthToken
                        })
                        {
                            _channelReader.OnMessageReceived += ReaderOnMessageReceived;

                            _channelWriter.Authenticate();

                            SendTwitchCommand("CAP REQ :twitch.tv/membership");
                            SendTwitchCommand("CAP REQ :twitch.tv/tags twitch.tv/commands");

                            OnBotConnected?.Invoke(this);

                            await _channelReader.ListenForMessages(cancellationToken);
                        }
                    }

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
            await Task.Run(() => _channelReader?.SignalShutdown());
        }

        private void ReaderOnMessageReceived(ChannelReader sender, MessageReceivedArgs args)
        {
            if (args.Message.StartsWith("PING"))
            {
                SendTwitchCommand("PONG", false);
            }
            else
            {
                if (TwitchCommandParser.IsMatch(args.Message))
                {
                    var message = TwitchCommandParser.Gather(args.Message);

                    if (_configuration.Handlers.ContainsKey(message.MessageType))
                    {
                        foreach (var messageHandlerType in _configuration.Handlers[message.MessageType])
                        {
                            var handler =
                                (IRCHandler)_container.GetService(messageHandlerType);

                            if (handler.CanExecute(message))
                            {
                                handler.Handle(message);
                            }
                        }
                    }
                }
            }
        }

        public void SendToStream(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                _channelWriter.SendMessage(message);
            }
        }

        private void SendTwitchCommand(string command, bool awaitResponse = true)
        {
            _channelWriter.SendCommand(command);

            if (awaitResponse)
            {
                _channelReader.ReadLine();
            }
        }
    }
}