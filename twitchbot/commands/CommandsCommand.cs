using System.Linq;
using twitchbot.infrastructure;

namespace twitchbot.commands
{
    [TwitchCommand("commands")]
    public class CommandsCommand : ITwitchCommand
    {
        private readonly CommandFactory _factory;

        public CommandsCommand(CommandFactory factory)
        {
            _factory = factory;
        }

        public string Execute(params string[] args)
        {
            return $"Available commands; {string.Join(", ", _factory.AvailableCommands)}";
        }
    }
}