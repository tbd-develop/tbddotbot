using System.Collections.Generic;
using System.Reflection;
using twitchstreambot.infrastructure;
using twitchstreambot.Parsing;

namespace twitchstreambot.Infrastructure
{
    public interface ICommandFactory
    {
        IReadOnlyCollection<string> AvailableCommands { get; }
        CommandFactory LoadFromAssembly(Assembly loadAssembly);
        ITwitchCommand GetCommand(TwitchMessage message);
        void AddTextCommand(string command);
    }
}