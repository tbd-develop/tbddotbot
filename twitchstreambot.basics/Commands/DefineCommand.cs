using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using twitchstreambot.Infrastructure;
using twitchstreambot.Infrastructure.Attributes;
using twitchstreambot.models;
using twitchstreambot.Parsing;

namespace twitchstreambot.command.Commands
{
    [TwitchCommand("define", IsPrivate = true)]
    public class DefineCommand : ITwitchCommand
    {
        public bool CanExecute(TwitchMessage message)
        {
            if (message.Headers.ContainsKey("badges"))
            {
                var badges = message.Headers["badges"].ToLower();

                return badges.Contains("broadcaster") ||
                       badges.Contains("moderator");
            }

            return false;
        }

        public string Execute(TwitchMessage message)
        {
            var commandArguments = message.Command.Arguments;

            if (!commandArguments.Any() || commandArguments.Count() < 2)
            {
                return "define <command> <content>";
            }

            string definedCommand = commandArguments.ElementAt(0).ToLower();

            string content = string.Join(" ", commandArguments.Skip(1));

            var definitions = LoadDefinitions();
            var currentDefinition = definitions.SingleOrDefault(d =>
                d.Command.Equals(definedCommand, StringComparison.CurrentCultureIgnoreCase));

            if (currentDefinition == null)
            {
                definitions.Add(new CommandDefinition { Command = definedCommand, Content = content });

                SaveDefinitions(definitions);
            }
            else
            {
                currentDefinition.Content = content;

                SaveDefinitions(definitions);
            }

            return null;
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
            {
                writer.Write(JsonConvert.SerializeObject(definitions));
            }
        }
    }
}