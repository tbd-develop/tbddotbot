using twitchstreambot.Parsing;

namespace twitchstreambot.Infrastructure
{
    public interface ITwitchCommand
    {
        bool CanExecute(TwitchMessage message);
        string Execute(TwitchMessage message);
    }
}