using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using twitchstreambot.Infrastructure;
using twitchstreambot.Parsing;

namespace twitchstreambot.Dispatch;

public class DefaultMessageDispatcher : IMessageDispatcher
{
    private readonly IMessagingPipeline _messagingPipeline;
    private readonly ICommandLookup _commandLookup;
    private readonly IServiceProvider _serviceProvider;

    public DefaultMessageDispatcher(
        IMessagingPipeline messagingPipeline,
        ICommandLookup commandLookup,
        IServiceProvider serviceProvider)
    {
        _messagingPipeline = messagingPipeline;
        _commandLookup = commandLookup;
        _serviceProvider = serviceProvider;
    }

    public async Task Dispatch(string message, CancellationToken cancellationToken = default)
    {
        if (!TwitchCommandParser.TryMatch(message, out var twitchMessage)) return;

        if (twitchMessage is null)
            return;

        var context = new MessagingContext(twitchMessage);

        await _messagingPipeline.Dispatch(context, cancellationToken);

        // if (twitchMessage is { IsBotCommand: false }) return MessageResult.NoResponse(true);
        //
        // var action = twitchMessage?.Command!.Action;
        //
        // if (action == null || !_commandLookup.TryGetCommand(action, out var commandType))
        //     return MessageResult.NoResponse(true);
        //
        // try
        // {
        //     if (_serviceProvider.GetRequiredService(commandType!) is not ITwitchCommand commandHandler)
        //         return MessageResult.NoResponse(true);
        //
        //     if (!commandHandler.CanExecute(twitchMessage!))
        //         return MessageResult.NoResponse(true);
        //
        //     var response = commandHandler.Execute(twitchMessage!);
        //
        //     return MessageResult.Respond(response);
        // }
        // catch (Exception exception)
        // {
        //     Console.WriteLine(exception);
        // }
        //
        // return MessageResult.NoResponse(true);
    }
}