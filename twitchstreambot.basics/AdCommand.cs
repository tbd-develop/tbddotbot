using twitchstreambot.Infrastructure;
using twitchstreambot.Infrastructure.Attributes;
using twitchstreambot.Parsing;

namespace twitchstreambot.basics;

[TwitchCommand("ad")]
public class AdCommand : ITwitchCommand
{
    public bool CanExecute(TwitchMessage message)
    {
        return true;
    }

    public string Execute(TwitchMessage message)
    {
        return "Twitch wants ads run, so they're running. If you have any questions, please feel free to ask.";
    }
}