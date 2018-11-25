using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using twitchbot.infrastructure;
using twitchbot.models;

namespace twitchbot.commands
{
    [TwitchCommand("recall", Ignore = true)]
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
                    IEnumerable<CommandDefinition> definedCommmands =
                        JsonConvert.DeserializeObject<IEnumerable<CommandDefinition>>(content);

                    result = definedCommmands.Single(cmd =>
                        cmd.Command.Equals(command, StringComparison.CurrentCultureIgnoreCase)).Content;
                }
            }

            return result;
        }
    }
}