using System;
using System.Linq;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;
using Sprache;
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

        public event CommandReceivedHandler OnCommandReceived;

        public string Error { get; private set; }

        public TwitchStreamBot(TwitchConnection connection, string authToken, CommandFactory commandFactory)
        {
            _connection = connection;
            _authToken = authToken;
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

                    if (result.IrcCommand == TwitchCommand.PRIVMSG)
                    {
                        if (result.Message.StartsWith("!"))
                        {
                            string[] elements = result.Message.Substring(1).Split(' ');

                            var command = _commandFactory.GetCommand(elements[0]);

                            if (command != null)
                            {
                                if (command.CanExecute(result.Headers))
                                {
                                    SendToStream(command.Execute(elements.Skip(1).ToArray()));
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ReaderOnCommandReceived(ChannelReader sender, CommandArgs args)
        {
            var commandToExecute = _commandFactory.GetCommand(args.Command);

            if (commandToExecute != null)
            {
                string response = "Can't help you with that one";

                if (commandToExecute.CanExecute(args.Headers))
                {
                    response = commandToExecute.Execute(args.Arguments.ToArray());
                }

                SendToStream(response);
            }

            OnCommandReceived?.Invoke(this, args);
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