using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using twitchstreambot.Commands;
using twitchstreambot.Infrastructure;
using twitchstreambot.infrastructure.DependencyInjection;
using twitchstreambot.models;
using twitchstreambot.Parsing;

namespace twitchstreambot.infrastructure
{
    public class CommandFactory : ICommandFactory
    {
        private readonly IContainer _container;
        private readonly IDictionary<string, CommandInfo> _commands;
        private readonly IList<string> _customCommands;

        public IReadOnlyCollection<string> AvailableCommands =>
            new ReadOnlyCollection<string>(_commands.Where(c => c.Value.IsListed).Select(s => s.Key)
                .Union(_customCommands).ToList());

        public CommandFactory(IContainer container)
        {
            _container = container;
            _commands = new Dictionary<string, CommandInfo>();
            _customCommands = new List<string>();

            RegisterCommandsInAssembly(Assembly.GetExecutingAssembly());

            LoadCustomCommands();
        }

        public CommandFactory LoadFromAssembly(Assembly loadAssembly)
        {
            RegisterCommandsInAssembly(loadAssembly);

            return this;
        }

        public ITwitchCommand GetCommand(TwitchMessage message)
        {
            if (_commands.ContainsKey(message.Command.Action))
            {
                return (ITwitchCommand)_container.GetInstance(_commands[message.Command.Action].CommandType,
                    new Object[] { message.Headers });
            }

            if (_customCommands.Contains(message.Command.Action))
            {
                return new RecallCommand(message.Command.Action);
            }

            return null;
        }

        public void AddTextCommand(string command)
        {
            _customCommands.Add(command);
        }

        private void RegisterCommandsInAssembly(Assembly assembly)
        {
            var commands = from t in assembly.GetTypes()
                let attribute = t.GetCustomAttribute<TwitchCommandAttribute>()
                where attribute != null && !string.IsNullOrEmpty(attribute.Action) && !attribute.Ignore
                select new
                {
                    Action = attribute.Action,
                    attribute.IsPrivate,
                    Type = t
                };

            foreach (var command in commands)
            {
                _commands.Add(command.Action,
                    new CommandInfo { CommandType = command.Type, IsListed = !command.IsPrivate });
            }
        }

        private void LoadCustomCommands()
        {
            if (File.Exists("definitions.json"))
            {
                using (StreamReader reader = new StreamReader("definitions.json"))
                {
                    string content = reader.ReadToEnd();

                    if (!string.IsNullOrEmpty(content))
                    {
                        IEnumerable<CommandDefinition> definedCommands =
                            JsonConvert.DeserializeObject<IEnumerable<CommandDefinition>>(content);

                        foreach (var definition in definedCommands)
                        {
                            _customCommands.Add(definition.Command);
                        }
                    }
                }
            }
        }

        internal class CommandInfo
        {
            public Type CommandType { get; set; }
            public bool IsListed { get; set; }
        }

    }
}