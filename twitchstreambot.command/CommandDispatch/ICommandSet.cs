using System;
using twitchstreambot.Parsing;

namespace twitchstreambot.command.CommandDispatch
{
    public interface ICommandSet
    {
        Type GetCommand(TwitchMessage message);
    }
}