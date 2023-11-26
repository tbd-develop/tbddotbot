using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using twitchstreambot.Infrastructure;

namespace twitchstreambot.Middleware;

public class CommandMiddleware : IMessagingMiddleware
{
    private readonly ICommandLookup _commandLookup;
    private readonly IServiceProvider _serviceProvider;
    private readonly IStreamOutput _streamOutput;

    public CommandMiddleware(
        ICommandLookup commandLookup,
        IServiceProvider serviceProvider,
        IStreamOutput streamOutput)
    {
        _commandLookup = commandLookup;
        _serviceProvider = serviceProvider;
        _streamOutput = streamOutput;
    }

    public ValueTask<MessageResult> Execute(MessagingContext context, CancellationToken cancellationToken = default)
    {
        var twitchMessage = context.Message;

        var result = MessageResult.NoAction();

        if (twitchMessage is { IsBotCommand: false })
        {
            return ValueTask.FromResult(result);
        }

        var action = twitchMessage.Command!.Action;

        if (!_commandLookup.TryGetCommand(action, out var commandType))
        {
            return ValueTask.FromResult(result);
        }

        if (_serviceProvider.GetRequiredService(commandType!) is not ITwitchCommand commandHandler)
        {
            return ValueTask.FromResult(MessageResult.Error($"Unable to find handler {commandType}"));
        }

        if (!commandHandler.CanExecute(twitchMessage))
        {
            return ValueTask.FromResult(result);
        }

        var response = commandHandler.Execute(twitchMessage);

        _streamOutput.SendToStream(response);

        return ValueTask.FromResult(MessageResult.Success());
    }
}