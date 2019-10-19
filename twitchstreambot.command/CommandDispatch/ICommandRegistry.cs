using System;

namespace twitchstreambot.command.CommandDispatch
{
    public interface ICommandRegistry
    {
        bool CanProvide(string command);
        Type Get(string command);
    }
}