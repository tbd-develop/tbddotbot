using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using twitchbot.infrastructure;
using twitchbot.models;

namespace twitchbot
{
    public class TwitchStreamBot
    {
        private readonly TwitchConnection _connection;
        private readonly string _authToken;
        private readonly CommandFactory _commandFactory;

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
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream) {AutoFlush = true})
                {
                    Authenticate(writer);

                    if (!VerifyConnected(reader))
                    {
                        return -1;
                    }

//                    SendTwitchCommand(writer, reader, "CAP REQ :twitch.tv/membership");
//                    SendTwitchCommand(writer, reader, "CAP REQ :twitch.tv/tags twitch.tv/commands");

                    ListenForMessages(reader, writer);
                }
            }

            return 0;
        }

        private void SendTwitchCommand(StreamWriter writer, StreamReader reader, string command)
        {
            writer.SendCommand(command);

            string response = reader.ReadLine();

            Console.WriteLine(response);
        }

        private void Authenticate(StreamWriter writer)
        {
            writer.WriteLine($"PASS oauth:{_authToken}");
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
            string inputBuffer = string.Empty;
            string basicMessageRegex =
                @":(?<user>.*)!(.*)@(.*)\.tmi\.twitch\.tv\s(?<irccommand>.*)\s#(?<channel>.*)\s:!(?<command>[\w\d]+)\s?(?<arguments>.*)?";
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
                            var instance = _commandFactory.GetCommand(commandName);

                            if (instance != null)
                            {
                                string commandResult = string.Empty;

                                if (matches.Groups["arguments"].Success)
                                {
                                    commandResult = instance.Execute(matches.Groups["arguments"].Value.Split(' '));
                                }
                                else
                                {
                                    commandResult = instance.Execute();
                                }

                                if (!string.IsNullOrEmpty(commandResult))
                                {
                                    writer.SendMessage(_connection.Channel, commandResult);
                                }
                            }
                            else
                            {
                                if (commandName.Equals("commands", StringComparison.CurrentCultureIgnoreCase))
                                {
                                    writer.SendMessage(_connection.Channel,
                                        $"Available commands; {string.Join(",", _commandFactory.AvailableCommands)}");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine(inputBuffer);
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