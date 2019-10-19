using System;

namespace twitchstreambot.Infrastructure.@new
{
    public interface ICommandRegistry
    {
        bool CanProvide(string command);
        Type Get(string command);
    }
}