using twitchbot.Infrastructure;
using twitchstreambot;

namespace twitchbot.timers
{
    [TwitchTimer(300)]
    public class RemindDiscord : ITwitchTimerAction
    {
        public void OnTimer(TwitchStreamBot bot)
        {
            bot.SendToStream($"Join our discord https://discord.gg/qAUxY65");
        }
    }
}