using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using twitchstreambot.basics.Infrastructure;
using twitchstreambot.Infrastructure;
using twitchstreambot.Infrastructure.Attributes;
using twitchstreambot.models;
using twitchstreambot.Parsing;

namespace twitchstreambot.command.Commands
{
    [TwitchCommand("define", IsPrivate = true)]
    public class DefineCommand : ITwitchCommand
    {
        private readonly DefinedCommandsStore _commandsStore;

        public DefineCommand(DefinedCommandsStore commandsStore)
        {
            _commandsStore = commandsStore;
        }

        public bool CanExecute(TwitchMessage message)
        {
            return message.IsInRole(StreamRole.Moderator, StreamRole.Broadcaster);
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
            string content = _commandsStore.Load();

            if (!string.IsNullOrEmpty(content))
            {
                return new List<CommandDefinition>(
                    JsonConvert.DeserializeObject<IEnumerable<CommandDefinition>>(content));
            }

            return new List<CommandDefinition>();
        }

        private void SaveDefinitions(IEnumerable<CommandDefinition> definitions)
        {
            _commandsStore.Save(JsonConvert.SerializeObject(definitions));
        }
    }
}