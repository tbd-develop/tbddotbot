using twitchstreambot.Infrastructure;
using twitchstreambot.Parsing;

namespace twitchbot.tests.ConcerningCommandExecutor.Commands
{
    public class BasicCommand : ITwitchCommand
    {
        private readonly string _messageToReturn;

        public BasicCommand(string messageToReturn)
        {
            _messageToReturn = messageToReturn;
        }

        public bool CanExecute(TwitchMessage message)
        {
            return true;
        }

        public string Execute(TwitchMessage message)
        {
            return _messageToReturn;
        }
    }
}