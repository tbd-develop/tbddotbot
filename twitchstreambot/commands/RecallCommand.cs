using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using twitchstreambot.infrastructure;
using twitchstreambot.models;

namespace twitchstreambot.Commands
{
    [TwitchCommand("recall", Ignore = true, IsPrivate = true)]
    public class RecallCommand : ITwitchCommand
    {
        private readonly string _command;

        public RecallCommand()
        {
            _command = String.Empty;
        }

        public RecallCommand(string command)
        {
            _command = command;
        }

        public bool CanExecute()
        {
            return true;
        }

        public string Execute(params string[] args)
        {
            string commandDefinition = string.Empty;

            if (!string.IsNullOrEmpty(_command))
            {
                commandDefinition = LoadCommand(_command);
            }
            else if (args.Length > 0)
            {
                commandDefinition = LoadCommand(args[0]);
            }

            return commandDefinition;
        }

        private string LoadCommand(string command)
        {
            string result = string.Empty;

            using (StreamReader reader = new StreamReader("definitions.json"))
            {
                string content = reader.ReadToEnd();

                if (!string.IsNullOrEmpty(content))
                {
                    IEnumerable<CommandDefinition> definedCommands =
                        JsonConvert.DeserializeObject<IEnumerable<CommandDefinition>>(content);

                    var definition = definedCommands.SingleOrDefault(cmd =>
                        cmd.Command.Equals(command, StringComparison.CurrentCultureIgnoreCase));

                    result = definition?.Content ?? "No Command Found";
                }
            }

            return result;
        }
    }
}