using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using twitchbot.commands;
using twitchbot.infrastructure.DependencyInjection;
using twitchbot.models;

namespace twitchbot.infrastructure
{
    public class CommandFactory
    {
        private readonly IContainer _container;
        private readonly IDictionary<string, Type> _commands;
        private readonly IList<string> _customCommands;

        public IReadOnlyCollection<string> AvailableCommands =>
            new ReadOnlyCollection<string>(_commands.Keys.Union(_customCommands).OrderBy(r => r).ToList());

        public CommandFactory(IContainer container)
        {
            _container = container;
            _commands = new Dictionary<string, Type>();
            _customCommands = new List<string>();

            RegisterAvailableCommands();
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

            LoadCustomCommands();
        }

        public ITwitchCommand GetCommand(string command)
        {
            if (_commands.ContainsKey(command))
            {
                return (ITwitchCommand) _container.GetInstance(_commands[command]);
            }

            if (_customCommands.Contains(command))
            {
                return new RecallCommand(command);
            }

            return null;
        }

        public void AddTextCommand(string command)
        {
            _customCommands.Add(command);
        }

        private void LoadCustomCommands()
        {
            using (StreamReader reader = new StreamReader("definitions.json"))
            {
                string content = reader.ReadToEnd();

                if (!string.IsNullOrEmpty(content))
                {
                    IEnumerable<CommandDefinition> definedCommmands =
                        JsonConvert.DeserializeObject<IEnumerable<CommandDefinition>>(content);

                    foreach (var definition in definedCommmands)
                    {
                        _customCommands.Add(definition.Command);
                    }
                }
            }
        }
    }
}