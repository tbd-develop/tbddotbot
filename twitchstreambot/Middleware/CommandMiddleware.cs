using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using twitchstreambot.Dispatch;
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

    public async Task Execute(MessagingContext context,
        MiddlewareDelegate next,
        CancellationToken cancellationToken = default)
    {
        var twitchMessage = context.Message;

        if (twitchMessage is { IsBotCommand: false }) return;

        var action = twitchMessage?.Command!.Action;

        if (action == null || !_commandLookup.TryGetCommand(action, out var commandType))
            return;

        try
        {
            if (_serviceProvider.GetRequiredService(commandType!) is not ITwitchCommand commandHandler)
                return;

            if (!commandHandler.CanExecute(twitchMessage!))
                return;

            string response = commandHandler.Execute(twitchMessage!);

            _streamOutput.WriteToStream(response);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }
}

public interface IStreamOutput
{
    void WriteToStream(string message);
}