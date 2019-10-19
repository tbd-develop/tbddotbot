using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using twitchstreambot.command.CommandDispatch;
using twitchstreambot.command.Commands;
using twitchstreambot.models;

namespace twitchstreambot.basics
{
    public class DefinedCommandsRegistry : ICommandRegistry
    {
        private IList<string> _availableCommands;

        public DefinedCommandsRegistry()
        {
            _availableCommands = new List<string>(LoadDefinitions().Select(c => c.Command));
        }

        public bool CanProvide(string command)
        {
            return _availableCommands.Contains(command.ToLower());
        }

        public Type Get(string command)
        {
            return typeof(RecallCommand);
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
    }
}