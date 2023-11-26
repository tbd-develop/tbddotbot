using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using twitchstreambot.Infrastructure;

namespace twitchstreambot.Dispatch;

public class MessagingPipeline : IMessagingPipeline
{
    private readonly IEnumerable<IMessagingMiddleware> _middlewares;

    public MessagingPipeline(IEnumerable<IMessagingMiddleware> middlewares)
    {
        _middlewares = middlewares;
    }

    public async Task Dispatch(MessagingContext context,
        CancellationToken cancellationToken = default)
    {
        foreach (var middleware in _middlewares)
        {
            var response = await middleware.Execute(context, cancellationToken);

            if (response.IsError)
            {
                break;
            }
        }
    }
}