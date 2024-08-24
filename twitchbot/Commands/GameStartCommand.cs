using twitchbot.Infrastructure;
using twitchstreambot.Infrastructure;
using twitchstreambot.Infrastructure.Attributes;
using twitchstreambot.Infrastructure.Extensions;
using twitchstreambot.Parsing;

namespace twitchbot.Commands;

[TwitchCommand("gamestart", Ignore = true)]
public class GameStartCommand(GameState state) : ITwitchCommand
{
    public bool CanExecute(TwitchMessage message) =>
        message.IsInRole(StreamRole.Broadcaster, StreamRole.Subscriber, StreamRole.Vip);

    public string Execute(TwitchMessage message)
    {
        if (state.IsStarted)
        {
            return "Game is already in progress";
        }

        state.Start(message.User.Name);

        return string.Empty;
    }
}