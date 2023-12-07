using System.Threading;
using System.Threading.Tasks;
using twitchstreambot.Infrastructure;
using twitchstreambot.Parsing;

namespace twitchstreambot.Dispatch;

public class DefaultMessageDispatcher : IMessageDispatcher
{
    private readonly IMessagingPipeline _messagingPipeline;

    public DefaultMessageDispatcher(
        IMessagingPipeline messagingPipeline)
    {
        _messagingPipeline = messagingPipeline;
    }

    public async Task Dispatch(string message, CancellationToken cancellationToken = default)
    {
        if (!TwitchCommandParser.TryMatch(message, out var twitchMessage)) return;

        if (twitchMessage is null)
            return;

        var context = new MessagingContext(twitchMessage);

        await _messagingPipeline.Dispatch(context, cancellationToken);
    }
}