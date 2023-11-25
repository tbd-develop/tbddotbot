using System;
using Microsoft.Extensions.DependencyInjection;
using twitchstreambot.Infrastructure;
using twitchstreambot.Parsing;

namespace twitchstreambot.command.CommandDispatch
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ICommandSet _commandSet;

        public CommandDispatcher(
            ICommandSet commandSet,
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _commandSet = commandSet;
        }

        public bool CanExecute(TwitchMessage message) => _commandSet?.IsRegistered(message) ?? false;

        public string? ExecuteTwitchCommand(TwitchMessage twitchMessage)
        {
            var commandType = _commandSet.GetCommandType(twitchMessage);

            if (commandType is null)
                return null;

            if (_serviceProvider.GetRequiredService(commandType) is not ITwitchCommand command)
                throw new InvalidOperationException($"Command {commandType.Name} is not a valid command");

            return command.CanExecute(twitchMessage) ? command.Execute(twitchMessage) : null;
        }
    }
}