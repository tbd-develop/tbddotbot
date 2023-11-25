using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using twitchstreambot.command.CommandDispatch;
using twitchstreambot.Infrastructure.Attributes;
using twitchstreambot.Parsing;

namespace twitchstreambot.basics
{
    public class BasicCommandSet : ICommandSet
    {
        private Dictionary<string, Type> _availableCommands = null!;

        public void RegisterCommands(IServiceCollection serviceCollection)
        {
            _availableCommands = (from t in GetType().Assembly.GetTypes()
                                  let attribute = t.GetCustomAttribute<TwitchCommandAttribute>()
                                  where attribute != null && !string.IsNullOrEmpty(attribute.Action) && !attribute.Ignore
                                  select new
                                  {
                                      Action = attribute.Action.ToLower(),
                                      Type = t
                                  }).ToDictionary(k => k.Action, k => k.Type);

            foreach (var command in _availableCommands)
            {
                serviceCollection.AddTransient(command.Value);
            }
        }

        public Type GetCommandType(TwitchMessage message)
        {
            return _availableCommands[message.Command.Action.ToLower()];
        }

        public bool IsRegistered(TwitchMessage message)
        {
            return _availableCommands.ContainsKey(message.Command.Action.ToLower());
        }
    }
}