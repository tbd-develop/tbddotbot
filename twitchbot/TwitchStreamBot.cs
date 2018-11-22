using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using twitchbot.infrastructure;
using twitchbot.models;

namespace twitchbot
{
    public class TwitchStreamBot
    {
        private readonly TwitchConnection _connection;
        private readonly Auth _authentication;
        private readonly IDictionary<string, Type> _commands;

        public string Error { get; private set; }

        public TwitchStreamBot(TwitchConnection connection, Auth authentication)
        {
            _connection = connection;
            _authentication = authentication;
            _commands = new Dictionary<string, Type>();
        }

        public int Start()
        {
            using (var client = new TcpClient(_connection.HostName, _connection.Port))
            {
                using (var stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream) {AutoFlush = true})
                {
                    Authenticate(writer);

                    if (!VerifyConnected(reader))
                    {
                        return -1;
                    }

                    ListenForMessages(reader, writer);
                }
            }

            return 0;
        }

        public void RegisterCommand<T>(string command)
            where T : ITwitchCommand
        {
            _commands.Add(command, typeof(T));
        }

        private void Authenticate(StreamWriter writer)
        {
            writer.WriteLine($"PASS oauth:{_authentication.AuthToken}");
            writer.WriteLine($"NICK {_connection.BotName}");
            writer.WriteLine($"JOIN #{_connection.Channel}");
        }

        private bool VerifyConnected(StreamReader reader)
        {
            bool result = false;
            string inputBuffer = string.Empty;
            string verifyRegex =
                @":(?<address>.*)\s(?<code>[\d]*)\s(?<botname>.*)\s#(?<channel>.*)\s:End of \/NAMES list";

            try
            {
                while ((inputBuffer = reader.ReadLine()) != null)
                {
                    var match = Regex.Match(inputBuffer, verifyRegex);

                    if (match.Success)
                    {
                        if (match.Groups["code"].Value == "366")
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            catch (TimeoutException exception)
            {
                Error = exception.Message;
            }

            return result;
        }

        private void ListenForMessages(StreamReader reader, StreamWriter writer)
        {
            bool result = false;
            string inputBuffer = string.Empty;
            string basicMessageRegex =
                @":(?<user>.*)!(.*)@(.*)\.tmi\.twitch\.tv\s(?<irccommand>.*)\s#(?<channel>.*)\s:!(?<command>.*)";
            try
            {
                while ((inputBuffer = reader.ReadLine()) != null)
                {
                    if (inputBuffer.StartsWith("ping", StringComparison.CurrentCultureIgnoreCase)) // user can ping
                    {
                        writer.WriteLine("PONG :tmi.twitch.tv");
                        writer.Flush();
                    }
                    else
                    {
                        var matches = Regex.Match(inputBuffer, basicMessageRegex);

                        if (matches.Success)
                        {
                            var commandName = matches.Groups["command"].Value;

                            if (_commands.ContainsKey(commandName))
                            {
                            }
                            else
                            {
                                if (commandName.Equals("commands", StringComparison.CurrentCultureIgnoreCase))
                                {
                                    writer.SendMessage(_connection.Channel,
                                        $"Available commands; {string.Join(",", _commands.Keys)}");
                                }
                            }
                        }
                    }
                }
            }
            catch (TimeoutException exception)
            {
                Error = exception.Message;
            }
        }
    }
}