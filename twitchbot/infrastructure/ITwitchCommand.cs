using System.Collections.Generic;

namespace twitchbot.infrastructure
{
    public interface ITwitchCommand
    {
        bool CanExecute(IDictionary<string, string> headers);
        string Execute(params string[] args);
    }
}