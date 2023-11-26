using System;
using Microsoft.Extensions.DependencyInjection;
using twitchstreambot.Infrastructure;
using twitchstreambot.Parsing;

namespace twitchstreambot.Dispatch;

public class DefaultMessageDispatcher : IMessageDispatcher
{
    private readonly ICommandLookup _commandLookup;
    private readonly IServiceProvider _serviceProvider;

    public DefaultMessageDispatcher(ICommandLookup commandLookup, IServiceProvider serviceProvider)
    {
        _commandLookup = commandLookup;
        _serviceProvider = serviceProvider;
    }

    public MessageResult Dispatch(string message)
    {
#if DEBUG
        Console.WriteLine(message);
#endif

        if (!TwitchCommandParser.TryMatch(message, out var twitchMessage)) return MessageResult.NoResponse();

        if (twitchMessage is { IsBotCommand: false }) return MessageResult.NoResponse(true);

        var action = twitchMessage?.Command!.Action;

        if (action == null || !_commandLookup.TryGetCommand(action, out var commandType))
            return MessageResult.NoResponse(true);

        try
        {
            if (_serviceProvider.GetRequiredService(commandType!) is not ITwitchCommand commandHandler)
                return MessageResult.NoResponse(true);

            if (!commandHandler.CanExecute(twitchMessage!))
                return MessageResult.NoResponse(true);

            var response = commandHandler.Execute(twitchMessage!);

            return MessageResult.Respond(response);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }

        return MessageResult.NoResponse(true);
    }
}