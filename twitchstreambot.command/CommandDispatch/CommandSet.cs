using System;
using System.Linq;
using twitchstreambot.Infrastructure;
using twitchstreambot.Parsing;

namespace twitchstreambot.command.CommandDispatch
{
    public class CommandSet : ICommandSet
    {
        private readonly CommandRegistry[] _registries;

        public CommandSet(CommandRegistry[] registries)
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