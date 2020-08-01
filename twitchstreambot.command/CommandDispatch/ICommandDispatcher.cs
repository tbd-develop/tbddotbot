using twitchstreambot.Parsing;

namespace twitchstreambot.command.CommandDispatch
{
    public interface ICommandDispatcher
    {
        bool CanExecute(TwitchMessage message);
        string ExecuteTwitchCommand(TwitchMessage twitchMessage);
    }
}