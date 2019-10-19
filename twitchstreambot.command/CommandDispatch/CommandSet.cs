using System;
using System.Linq;
using twitchstreambot.Infrastructure;
using twitchstreambot.Parsing;

namespace twitchstreambot.command.CommandDispatch
{
    public class CommandSet : ICommandSet
    {
        private readonly ICommandRegistry[] _registries;

        public CommandSet(ICommandRegistry[] registries)
        {
            Guard.IsNotEmpty(registries, "At least one registry must be provided for commands");

            _registries = registries;
        }

        public Type GetCommand(TwitchMessage message)
        {
            var registry = _registries.SingleOrDefault(r => r.CanProvide(message.Command.Action));

            return registry?.Get(message.Command.Action);
        }
    }
}