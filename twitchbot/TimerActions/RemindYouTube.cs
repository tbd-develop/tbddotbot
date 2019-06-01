using twitchbot.Infrastructure;
using twitchstreambot;

namespace twitchbot.timers
{
    [TwitchTimer(600)]
    public class RemindYouTube : ITwitchTimerAction
    {
        public void OnTimer(TwitchStreamBot bot)
        {
            bot.SendToStream(
                $"Have you checked out our YouTube? https://www.youtube.com/channel/UCOsWJ6Nb13ihF8pgbh-DTAQ");
        }
    }
}