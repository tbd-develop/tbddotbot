using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using twitchbot.infrastructure.DependencyInjection;

namespace twitchbot.infrastructure
{
    public class CommandFactory
    {
        private readonly IContainer _container;
        private readonly IDictionary<string, Type> _commands;

        public IReadOnlyCollection<string> AvailableCommands => new ReadOnlyCollection<string>(_commands.Keys.ToList());

        public CommandFactory(IContainer container)
        {
            _container = container;
            _commands = new Dictionary<string, Type>();

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
        }

        public ITwitchCommand GetCommand(string command)
        {
            if (_commands.ContainsKey(command))
            {
                return (ITwitchCommand) _container.GetInstance(_commands[command]);
            }

            return null;
        }
    }
}