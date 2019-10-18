using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using twitchstreambot.infrastructure;
using twitchstreambot.models;

namespace twitchstreambot.Commands
{
    [TwitchCommand("define", IsPrivate = true)]
    public class DefineCommand : ITwitchCommand
    {
        private readonly CommandFactory _commandFactory;
        private readonly Dictionary<string, string> _headers;

        public DefineCommand(Dictionary<string, string> headers, CommandFactory commandFactory)
        {
            _commandFactory = commandFactory;
            _headers = headers;
        }

        public bool CanExecute()
        {
            if (_headers.ContainsKey("badges"))
            {
                var badges = _headers["badges"].ToLower();

                return badges.Contains("broadcaster") ||
                       badges.Contains("moderator");
            }

            return false;
        }

        public string Execute(params string[] args)
        {
            if (!args.Any() || args.Length < 2)
            {
                return "define <command> <content>";
            }

            string definedCommand = args[0].ToLower();
            string content = string.Join(" ", args.Skip(1));

            var definitions = LoadDefinitions();
            var currentDefinition = definitions.SingleOrDefault(d =>
                d.Command.Equals(definedCommand, StringComparison.CurrentCultureIgnoreCase));

            if (currentDefinition == null)
            {
                definitions.Add(new CommandDefinition { Command = definedCommand, Content = content });

                _commandFactory.AddTextCommand(definedCommand);

                SaveDefinitions(definitions);
            }
            else
            {
                currentDefinition.Content = content;

                SaveDefinitions(definitions);
            }

            return "Definition Added";
        }

        private List<CommandDefinition> LoadDefinitions()
        {
            using (StreamReader reader = new StreamReader("definitions.json"))
            {
                string result = reader.ReadToEnd();

                if (!string.IsNullOrEmpty(result))
                {
                    return new List<CommandDefinition>(
                        JsonConvert.DeserializeObject<IEnumerable<CommandDefinition>>(result));
                }

                return new List<CommandDefinition>();
            }
        }

        private void SaveDefinitions(IEnumerable<CommandDefinition> definitions)
        {
            using (StreamWriter writer = new StreamWriter("definitions.json", append: false))
                writer.Write(JsonConvert.SerializeObject(definitions));
        }
    }
}