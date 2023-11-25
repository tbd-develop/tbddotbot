using System;
using twitchstreambot.Parsing;

namespace twitchstreambot.command.CommandDispatch
{
    public interface ICommandSet
    {
        Type? GetCommandType(TwitchMessage message);
        bool IsRegistered(TwitchMessage message);
    }
}