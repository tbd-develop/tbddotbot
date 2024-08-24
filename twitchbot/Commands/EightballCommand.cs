using twitchstreambot.Infrastructure;
using twitchstreambot.Infrastructure.Attributes;
using twitchstreambot.Parsing;

namespace twitchbot.Commands;

[TwitchCommand("8ball")]
public class EightballCommand : ITwitchCommand
{
    public bool CanExecute(TwitchMessage message) => true;

    public string Execute(TwitchMessage message)
    {
        return "Yes";
    }
}