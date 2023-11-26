using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using twitchstreambot.Infrastructure;

namespace twitchstreambot.Dispatch;

public delegate Task MiddlewareDelegate(MessagingContext context, Func<Task> next, CancellationToken cancellationToken = default);

public interface IMessagingMiddleware
{
    Task Execute(MessagingContext context, MiddlewareDelegate next, CancellationToken cancellationToken = default);
}

public class MessagingPipeline : IMessagingPipeline
{
    private readonly IEnumerable<MiddlewareDelegate> _middlewares;

    public MessagingPipeline(IEnumerable<MiddlewareDelegate> middlewares)
    {
        _middlewares = middlewares;
    }

    public async Task Dispatch(MessagingContext context,
        CancellationToken cancellationToken = default)
    {
        int currentIndex = -1;

        async Task Next()
        {
            currentIndex++;

            if (currentIndex < _middlewares.Count())
            {
                await _middlewares.ElementAt(currentIndex)(
                    context,
                    Next,
                    cancellationToken);
            }
        }

        await Next();
    }
}