using System.Collections.Generic;

namespace twitchstreambot.infrastructure
{
    public interface ITwitchCommand
    {
        bool CanExecute(IDictionary<string, string> headers);
        string Execute(params string[] args);
    }
}