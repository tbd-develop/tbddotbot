using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using twitchstreambot.command.CommandDispatch;
using twitchstreambot.Infrastructure.Attributes;

namespace twitchstreambot.basics
{
    public class BasicsRegistry : ICommandRegistry
    {
        private readonly Dictionary<string, Type> _availableCommands;

        public BasicsRegistry()
        {
            _availableCommands = (from t in GetType().Assembly.GetTypes()
                                  let attribute = t.GetCustomAttribute<TwitchCommandAttribute>()
                                  where attribute != null && !string.IsNullOrEmpty(attribute.Action) && !attribute.Ignore
                                  select new
                                  {
                                      Action = attribute.Action.ToLower(),
                                      Type = t
                                  }).ToDictionary(k => k.Action, k => k.Type);
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