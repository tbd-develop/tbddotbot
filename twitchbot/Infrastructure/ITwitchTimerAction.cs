using twitchstreambot;

namespace twitchbot.Infrastructure
{
    public interface ITwitchTimerAction
    {
        void OnTimer(TwitchStreamBot bot);
    }
}