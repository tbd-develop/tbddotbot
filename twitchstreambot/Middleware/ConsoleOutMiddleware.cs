using System;
using System.Threading;
using System.Threading.Tasks;
using twitchstreambot.Infrastructure;

namespace twitchstreambot.Middleware;

public class ConsoleOutMiddleware : IMessagingMiddleware
{
    public ValueTask<MessageResult> Execute(MessagingContext context, CancellationToken cancellationToken = default)
    {
        Console.WriteLine(context.Message.Content);

        return ValueTask.FromResult(MessageResult.Success());
    }
}