using System.Threading;
using System.Threading.Tasks;

namespace twitchstreambot.Infrastructure;

public interface IMessageDispatcher
{
    Task Dispatch(string message, CancellationToken cancellationToken = default);
}