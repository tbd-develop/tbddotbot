using System.Reflection;
using twitchstreambot.Api;
using twitchstreambot.webhooks.Infrastructure.Attributes;
using twitchstreambot.webhooks.Publishing.Contracts;

namespace twitchstreambot.webhooks.Infrastructure;

public class WebhookUserProfileProvider
{
    private readonly IEnumerable<string> _requiredScopes;
    private readonly TwitchApi _twitchApi;

    public WebhookUserProfileProvider(
        TwitchApi twitchApi,
        ILocalEventLookup localEventLookup)
    {
        _twitchApi = twitchApi;
        _requiredScopes = (from t in localEventLookup.EventTypes
                let attribute = t.GetCustomAttribute<WebhookEventAttribute>()
                where attribute != null
                select attribute.RequiredScopes).SelectMany(x => x)
            .Distinct()
            .ToList();
    }
}