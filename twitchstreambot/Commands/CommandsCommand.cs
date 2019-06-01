using System.Linq;
using twitchstreambot.infrastructure;

namespace twitchstreambot.Commands
{
    [TwitchCommand("commands")]
    public class CommandsCommand : ITwitchCommand
    {
        private readonly CommandFactory _factory;

        public CommandsCommand(CommandFactory factory)
        {
            _factory = factory;
        }

        public bool CanExecute()
        {
            return true;
        }

        public string Execute(params string[] args)
        {
            string commands = string.Join(", ", _factory.AvailableCommands.Except(new[] { "commands" }));

            return $"Available commands; {commands}";
        }
    }
}