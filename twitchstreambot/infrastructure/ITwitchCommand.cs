using System.Collections.Generic;

namespace twitchstreambot.infrastructure
{
    public interface ITwitchCommand
    {
        bool CanExecute();
        string Execute(params string[] args);
    }
}