using System;
using System.Linq;
using System.Net.Sockets;
using twitchstreambot.infrastructure;

namespace twitchstreambot
{
    public class TwitchStreamBot
    {
        private readonly TwitchConnection _connection;
        private readonly string _authToken;
        private readonly CommandFactory _commandFactory;
        private ChannelReader _channelReader;
        private ChannelWriter _streamWriter;

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
                using (_streamWriter = new ChannelWriter(stream)
                { Channel = _connection.Channel, BotName = _connection.BotName, AuthToken = _authToken })
                {
                    _channelReader.OnCommandReceived += ReaderOnCommandReceived;
                    _channelReader.OnMessageReceived += ReaderOnOnMessageReceived;

                    _streamWriter.Authenticate();

                    SendTwitchCommand(_streamWriter, _channelReader, "CAP REQ :twitch.tv/membership");
                    SendTwitchCommand(_streamWriter, _channelReader, "CAP REQ :twitch.tv/tags twitch.tv/commands");

                    _channelReader.ListenForMessages();
                }
            }

            return 0;
        }

        public void Stop()
        {
            _channelReader.SignalShutdown();
        }

        private void ReaderOnOnMessageReceived(ChannelReader sender, MessageReceivedArgs args)
        {
            if (!string.IsNullOrEmpty(args.Message))
            {
                Console.WriteLine($"{args.Message}");
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
                _streamWriter.SendMessage(message);
            }
        }

        private void SendTwitchCommand(ChannelWriter writer, ChannelReader reader, string command)
        {
            writer.SendCommand(command);

            string response = reader.ReadLine();

            Console.WriteLine(response);
        }
    }
}