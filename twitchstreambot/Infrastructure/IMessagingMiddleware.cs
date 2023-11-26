using System.Threading;
using System.Threading.Tasks;

namespace twitchstreambot.Infrastructure;

public interface IMessagingMiddleware
{
    ValueTask<MessageResult> Execute(MessagingContext context, CancellationToken cancellationToken = default);
}