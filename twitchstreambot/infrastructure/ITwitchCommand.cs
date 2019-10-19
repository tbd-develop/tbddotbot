using System.Collections.Generic;
using twitchstreambot.Parsing;

namespace twitchstreambot.infrastructure
{
    public interface ITwitchCommand
    {
        bool CanExecute(TwitchMessage message);
        string Execute(TwitchMessage message);
    }
}