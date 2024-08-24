using System.Linq;
using twitchstreambot.Infrastructure;
using twitchstreambot.Infrastructure.Attributes;
using twitchstreambot.Infrastructure.Extensions;
using twitchstreambot.Parsing;

namespace twitchbot.Commands;

[TwitchCommand("so")]
public class ShoutoutCommand : ITwitchCommand
{
    public bool CanExecute(TwitchMessage message) => message.IsInRole(StreamRole.Broadcaster);

    public string Execute(TwitchMessage message)
    {
        var user = message.Command.Arguments.First();

        return $"Check out {user} at https://twitch.tv/{user}";
    }
}