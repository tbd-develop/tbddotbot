using System.Diagnostics;
using twitchstreambot.webhooks.Infrastructure;
using twitchstreambot.webhooks.Infrastructure.Contracts;
using twitchstreambot.webhooks.Models;

namespace webhook_testing.Infrastructure;

public class SampleEventDispatcher(EventMapper mapper) : IWebhookEventDispatcher
{
    public Task Dispatch(IncomingSubscriptionMessage message,
        string data,
        CancellationToken cancellationToken = default)
    {
        var @event = mapper.Map(message, data);

        if (@event is not null)
        {
            Debugger.Break();
        }

        return Task.CompletedTask;
    }
}