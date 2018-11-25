using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using twitchbot.infrastructure;
using twitchbot.models;

namespace twitchbot.commands
{
    [TwitchCommand("define")]
    public class DefineCommand : ITwitchCommand
    {
        private readonly CommandFactory _commandFactory;

        public DefineCommand(CommandFactory commandFactory)
        {
            _commandFactory = commandFactory;
        }

        public bool CanExecute(IDictionary<string, string> headers)
        {
            if (headers.ContainsKey("badges"))
            {
                var badges = headers["badges"].ToLower();

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

            string command = args[0];
            string content = string.Join(" ", args.Skip(1));
            bool hasChanged = false;

            var definitions = LoadDefinitions();

            if (!definitions.Any(d => d.Command.Equals(command, StringComparison.CurrentCultureIgnoreCase)))
            {
                definitions.Add(new CommandDefinition {Command = command, Content = content});

                _commandFactory.AddTextCommand(command);

                hasChanged = true;
            }

            if (hasChanged)
            {
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