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
    [TwitchCommand("recall", Ignore = true, IsPrivate = true)]
    public class RecallCommand : ITwitchCommand
    {
        private readonly DefinedCommandsStore _commandsStore;

        public RecallCommand(DefinedCommandsStore commandsStore)
        {
            _commandsStore = commandsStore;
        }

        public bool CanExecute(TwitchMessage message)
        {
            return true;
        }

        public string Execute(TwitchMessage message)
        {
            string commandDefinition = string.Empty;

            if (!string.IsNullOrEmpty(message.Command.Action))
            {
                commandDefinition = LoadCommand(message.Command.Action);
            }

            return commandDefinition;
        }

        private string LoadCommand(string command)
        {
            string result = string.Empty;

            string content = _commandsStore.Load();

            if (!string.IsNullOrEmpty(content))
            {
                IEnumerable<CommandDefinition> definedCommands =
                    JsonConvert.DeserializeObject<IEnumerable<CommandDefinition>>(content);

                var definition = definedCommands.SingleOrDefault(cmd =>
                    cmd.Command.Equals(command, StringComparison.CurrentCultureIgnoreCase));

                result = definition?.Content ?? "No Command Found";
            }

            return result;
        }
    }
}