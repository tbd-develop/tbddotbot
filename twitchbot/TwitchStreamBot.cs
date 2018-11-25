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
        private readonly IDictionary<string, Type> _commands;
        private readonly IDictionary<string, ITwitchCommand> _instances;

        public string Error { get; private set; }

        public TwitchStreamBot(TwitchConnection connection, string authToken)
        {
            _connection = connection;
            _authToken = authToken;
            _commands = new Dictionary<string, Type>();
            _instances = new Dictionary<string, ITwitchCommand>();

            RegisterAvailableCommands();
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

        private void RegisterAvailableCommands()
        {
            var commands = from t in Assembly.GetExecutingAssembly().GetTypes()
                let attribute = t.GetCustomAttribute<TwitchCommandAttribute>()
                where attribute != null
                select new
                {
                    attribute.IdentifyWith,
                    Type = t
                };

            foreach (var command in commands)
            {
                _commands.Add(command.IdentifyWith, command.Type);
            }
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

                            if (_commands.ContainsKey(commandName))
                            {
                                if (!_instances.ContainsKey(commandName))
                                {
                                    var instance = BuildInstanceOfCommand(_commands[commandName]);

                                    if (instance != null)
                                    {
                                        _instances.Add(commandName, instance);
                                    }
                                }

                                if (_instances.ContainsKey(commandName))
                                {
                                    string commandResult = string.Empty;

                                    if (matches.Groups["arguments"].Success)
                                    {
                                        commandResult = _instances[commandName]
                                            .Execute(matches.Groups["arguments"].Value.Split(' '));
                                    }
                                    else
                                    {
                                        commandResult = _instances[commandName].Execute();
                                    }

                                    if (!string.IsNullOrEmpty(commandResult))
                                    {
                                        writer.SendMessage(_connection.Channel, commandResult);
                                    }
                                }
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

        private ITwitchCommand BuildInstanceOfCommand(Type commandType)
        {
            var constructorsWithParameters = (from c in commandType.GetConstructors()
                where c.GetParameters().Length > 0
                select c).ToList();

            if (constructorsWithParameters.Any())
            {
                var constructor = constructorsWithParameters.First();

                var parameters = from p in constructor.GetParameters()
                    let arg = (from x in GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                        where x.FieldType == p.ParameterType
                        select x).FirstOrDefault()
                    select arg.GetValue(this);

                return (ITwitchCommand) constructor.Invoke(parameters.ToArray());
            }

            return (ITwitchCommand) Activator.CreateInstance(commandType);
        }
    }
}