using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using twitchstreambot.Infrastructure;
using twitchstreambot.Infrastructure.Attributes;
using twitchstreambot.Parsing;

namespace twitchstreambot.command.CommandDispatch
{
    public class DefaultCommandSet : ICommandSet
    {
        private readonly IServiceCollection _serviceCollection;
        private readonly Dictionary<string, Type> _commands;

        public DefaultCommandSet(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
            _commands = new Dictionary<string, Type>();
        }

        public DefaultCommandSet AddCommandsFromAssembly(Assembly assembly)
        {
            var commands = GetAvailableCommands(assembly);

            foreach (var command in commands)
            {
                _commands.Add(command.Key, command.CommandType);

                _serviceCollection.AddTransient(command.CommandType);
            }

            return this;
        }

        private IEnumerable<CommandInfo> GetAvailableCommands(Assembly assembly)
        {
            return from t in assembly.GetTypes()
                where t.GetInterfaces().Any(i => i == typeof(ITwitchCommand))
                let parameters = t.GetCustomAttribute(typeof(TwitchCommandAttribute)) as TwitchCommandAttribute
                where parameters != null
                select new CommandInfo
                {
                    Key = parameters.Action.ToLower(),
                    CommandType = t
                };
        }

        public Type GetCommandType(TwitchMessage message)
        {
            return _commands[message.Command.Action.ToLower()];
        }

        public bool IsRegistered(TwitchMessage message)
        {
            return _commands.ContainsKey(message.Command.Action.ToLower());
        }

        private class CommandInfo
        {
            public string Key { get; set; }
            public Type CommandType { get; set; }
        }
    }
}