using twitchstreambot.Infrastructure;
using twitchstreambot.Parsing;

namespace twitchbot.tests.ConcerningCommandSet.Commands
{
    public class SampleCommand : ITwitchCommand
    {
        public bool CanExecute(TwitchMessage message)
        {
            throw new System.NotImplementedException();
        }

        public string Execute(TwitchMessage message)
        {
            throw new System.NotImplementedException();
        }
    }
}