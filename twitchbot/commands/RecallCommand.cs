using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using twitchbot.infrastructure;
using twitchbot.models;

namespace twitchbot.commands
{
    public class RecallCommand : ITwitchCommand
    {
        private readonly string _commandResult;

        public RecallCommand(string command)
        {
            using (StreamReader reader = new StreamReader("definitions.json"))
            {
                string content = reader.ReadToEnd();

                if (!string.IsNullOrEmpty(content))
                {
                    IEnumerable<CommandDefinition> definedCommmands =
                        JsonConvert.DeserializeObject<IEnumerable<CommandDefinition>>(content);

                    _commandResult = definedCommmands.Single(cmd =>
                        cmd.Command.Equals(command, StringComparison.CurrentCultureIgnoreCase)).Content;
                }
            }
        }


        public string Execute(params string[] args)
        {
            return _commandResult;
        }
    }
}