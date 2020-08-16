using System;
using twitchstreambot.Infrastructure;
using twitchstreambot.Parsing;

namespace twitchstreambot.command.CommandDispatch
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ICommandSet _commandSet;

        public CommandDispatcher(ICommandSet commandSet, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _commandSet = commandSet;
        }

        public bool CanExecute(TwitchMessage message) => _commandSet?.IsRegistered(message) ?? false;

        public string ExecuteTwitchCommand(TwitchMessage twitchMessage)
        {
            var commandType = _commandSet.GetCommand(twitchMessage);

            var command = (ITwitchCommand)_serviceProvider.GetService(commandType);

            return command.CanExecute(twitchMessage) ? command.Execute(twitchMessage) : string.Empty;
        }
    }
}