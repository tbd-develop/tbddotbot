using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using twitchstreambot.Commands;
using twitchstreambot.infrastructure;
using twitchstreambot.Infrastructure.@new;

namespace twitchstreambot.basics
{
    public class BasicsRegistry : ICommandRegistry
    {
        private readonly List<Type> _commandsToLoad = new List<Type> { typeof(ShoutOutCommand) };
        private readonly Dictionary<string, Type> _availableCommands;

        public BasicsRegistry()
        {
            _availableCommands = (from t in _commandsToLoad
                                  let attribute = t.GetCustomAttribute<TwitchCommandAttribute>()
                                  where attribute != null && !string.IsNullOrEmpty(attribute.Action) && !attribute.Ignore
                                  select new
                                  {
                                      Action = attribute.Action.ToLower(),
                                      Type = t
                                  }).ToDictionary(k => k.Action, k => k.Type);

            //foreach (var command in commands)
            //{
            //    _commands.Add(command.Action,
            //        new CommandInfo { CommandType = command.Type, IsListed = !command.IsPrivate });
            //}
        }

        public bool CanProvide(string command)
        {
            return _availableCommands.ContainsKey(command.ToLower());
        }

        public Type Get(string command)
        {
            return _availableCommands[command.ToLower()];
        }
    }
}