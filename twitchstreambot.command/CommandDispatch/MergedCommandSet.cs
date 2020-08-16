using System;
using System.Linq;
using twitchstreambot.Parsing;

namespace twitchstreambot.command.CommandDispatch
{
    /// <summary>
    /// If you've got more complicated commands than just a simple set of defined commands
    /// You might need to configure a custom CommandSet
    /// </summary>
    public class MergedCommandSet : ICommandSet
    {
        private readonly ICommandSet[] _commandSets;

        public MergedCommandSet(params ICommandSet[] commandSets)
        {
            _commandSets = commandSets;
        }

        public Type GetCommand(TwitchMessage message)
        {
            return _commandSets.FirstOrDefault(cs => cs.IsRegistered(message))?.GetCommand(message) ?? null;
        }

        public bool IsRegistered(TwitchMessage message)
        {
            return _commandSets.Any(cs => cs.IsRegistered(message));
        }
    }
}