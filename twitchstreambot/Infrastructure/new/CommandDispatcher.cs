using twitchstreambot.infrastructure;
using twitchstreambot.infrastructure.DependencyInjection;
using twitchstreambot.Parsing;

namespace twitchstreambot.Infrastructure.@new
{
    public class CommandDispatcher
    {
        private readonly ICommandSet _commandSet;
        private readonly IContainer _container;

        public CommandDispatcher(ICommandSet commandSet, IContainer container)
        {
            _commandSet = commandSet;
            _container = container;
        }

        public string SendTwitchCommand(TwitchMessage twitchMessage)
        {
            var commandType = _commandSet.GetCommand(twitchMessage);

            var command = (ITwitchCommand)_container.GetInstance(commandType);
            
            if (command?.CanExecute(twitchMessage) != null)
            {
                return command.Execute(twitchMessage);
            }

            return null;
        }
    }
}