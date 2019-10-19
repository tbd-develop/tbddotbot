using System;
using twitchstreambot.Parsing;

namespace twitchstreambot.Infrastructure.@new
{
    public interface ICommandSet
    {
        Type GetCommand(TwitchMessage message);
    }
}