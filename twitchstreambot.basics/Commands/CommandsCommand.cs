using System.Linq;
using twitchstreambot.infrastructure;
using twitchstreambot.Infrastructure;
using twitchstreambot.Parsing;

namespace twitchstreambot.Commands
{
    [TwitchCommand("commands")]
    public class CommandsCommand : ITwitchCommand
    {
        private readonly ICommandFactory _factory;

        public CommandsCommand(ICommandFactory factory)
        {
            _factory = factory;
        }

        public bool CanExecute(TwitchMessage message)
        {
            return true;
        }

        public string Execute(TwitchMessage message)
        {
            string commands = string.Join(", ", _factory.AvailableCommands.Except(new[] { "commands" }));

            return $"Available commands; {commands}";
        }
    }
}