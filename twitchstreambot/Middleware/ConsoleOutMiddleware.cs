using System;
using System.Threading;
using System.Threading.Tasks;
using twitchstreambot.Infrastructure;

namespace twitchstreambot.Middleware;

public class ConsoleOutMiddleware : IMessagingMiddleware
{
    public ValueTask<MessageResult> Execute(MessagingContext context, CancellationToken cancellationToken = default)
    {
        var message = context.Message;

        Console.WriteLine($"{message?.User?.Name} says {message?.Content}");

        return ValueTask.FromResult(MessageResult.Success());
    }
}