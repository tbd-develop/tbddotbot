using System;
using twitchstreambot.Infrastructure;
using twitchstreambot.Parsing;

namespace twitchstreambot.command.CommandDispatch
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly ICommandSet _commandSet;
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(ICommandSet commandSet, IServiceProvider serviceProvider)
        {
            _commandSet = commandSet;
            _serviceProvider = serviceProvider;
        }

        public bool CanExecute(TwitchMessage message) => _commandSet?.IsRegistered(message) ?? false;

        public string ExecuteTwitchCommand(TwitchMessage twitchMessage)
        {
            if (_commandSet.IsRegistered(twitchMessage))
            {
                var commandType = _commandSet.GetCommand(twitchMessage);

                var command = (ITwitchCommand)_serviceProvider.GetService(commandType);

                if (command != null && command.CanExecute(twitchMessage))
                {
                    return command.Execute(twitchMessage);
                }
            }

            return null;
        }
    }
}