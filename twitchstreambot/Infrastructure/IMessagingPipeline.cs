using System.Threading;
using System.Threading.Tasks;
using twitchstreambot.Dispatch;

namespace twitchstreambot.Infrastructure;

public interface IMessagingPipeline
{
    Task Dispatch(MessagingContext context, CancellationToken cancellationToken = default);
}

