using twitchstreambot.Parsing;

namespace twitchstreambot.Infrastructure
{
    public interface IRCHandler
    {
        void Handle(TwitchMessage message);
        bool CanExecute(TwitchMessage message);
    }
}