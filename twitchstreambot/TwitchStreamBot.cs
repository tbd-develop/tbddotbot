using System;
using System.Linq;
using System.Net.Sockets;
using Microsoft.Extensions.Configuration;
using twitchstreambot.infrastructure;
using twitchstreambot.Parsing;

namespace twitchstreambot
{
    public class TwitchStreamBot
    {
        private readonly TwitchConnection _connection;
        private readonly string _authToken;
        private readonly CommandFactory _commandFactory;
        private ChannelReader _channelReader;
        private ChannelWriter _channelWriter;

        public delegate void CommandReceivedHandler(TwitchStreamBot streamer, CommandArgs args);

        public delegate void BotConnectedHandler(TwitchStreamBot streamer);

        public event CommandReceivedHandler OnCommandReceived;
        public event BotConnectedHandler OnBotConnected;

        public string Error { get; private set; }

        public TwitchStreamBot(TwitchConnection connection, IConfiguration configuration, CommandFactory commandFactory)
        {
            _connection = connection;
            _authToken = configuration["twitch:auth"];
            _commandFactory = commandFactory;
        }

        public int Start()
        {
            using (var client = new TcpClient(_connection.HostName, _connection.Port))
            {
                using (var stream = client.GetStream())
                using (_channelReader = new ChannelReader(stream))
                using (_channelWriter = new ChannelWriter(stream)
                { Channel = _connection.Channel, BotName = _connection.BotName, AuthToken = _authToken })
                {
                    _channelReader.OnMessageReceived += ReaderOnMessageReceived;

                    _channelWriter.Authenticate();

                    SendTwitchCommand("CAP REQ :twitch.tv/membership");
                    SendTwitchCommand("CAP REQ :twitch.tv/tags twitch.tv/commands");

                    OnBotConnected?.Invoke(this);

                    _channelReader.ListenForMessages();
                }
            }

            return 0;
        }

        public void Stop()
        {
            _channelReader.SignalShutdown();
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
                    var result = TwitchCommandParser.Gather(args.Message);

                    if (result.IrcCommand == TwitchCommand.PRIVMSG && result.IsBotCommand)
                    {
                        var commandToExecute = _commandFactory.GetCommand(result);

                        if (commandToExecute != null && commandToExecute.CanExecute())
                        {
                            string response = commandToExecute.Execute(result.Command.Arguments.ToArray());

                            if (!string.IsNullOrEmpty(response))
                            {
                                SendToStream(response);
                            }
                        }
                    }

                    OnCommandReceived?.Invoke(this, new CommandArgs(result));
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
                string response = _channelReader.ReadLine();

                Console.WriteLine(response);
            }
        }
    }
}