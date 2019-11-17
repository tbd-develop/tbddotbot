using System;
using twitchstreambot.Infrastructure;
using twitchstreambot.Parsing;

namespace twitchstreambot.command.CommandDispatch
{
    public class CommandDispatcher
    {
        private readonly ICommandSet _commandSet;
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(ICommandSet commandSet, IServiceProvider serviceProvider)
        {
            _commandSet = commandSet;
            _serviceProvider = serviceProvider;
        }

        public string SendTwitchCommand(TwitchMessage twitchMessage)
        {
            var commandType = _commandSet.GetCommand(twitchMessage);

            var command = (ITwitchCommand)_serviceProvider.GetService(commandType);

            if (command != null && command.CanExecute(twitchMessage))
            {
                return command.Execute(twitchMessage);
            }

            return null;
        }
    }
}