using System.Reflection;
using System.Text.Json;
using twitchstreambot.webhooks.Events;
using twitchstreambot.webhooks.Infrastructure.Attributes;
using twitchstreambot.webhooks.Models;

namespace twitchstreambot.webhooks.Infrastructure;

public class EventMapper
{
    private static Dictionary<string, Type>? _eventTypes = null;
    private static bool IsInitialized => _eventTypes != null;

    private static JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private static void Initialize()
    {
        if (IsInitialized)
        {
            return;
        }

        _eventTypes =
            Assembly.GetAssembly(typeof(WebhookBaseEvent))
                ?.GetTypes()
                .Where(x => x is { IsClass: true, IsAbstract: false } && x.IsSubclassOf(typeof(WebhookBaseEvent)))
                .ToDictionary(x => x.GetCustomAttribute<WebhookEventAttribute>()!.Event);
    }


    public WebhookBaseEvent? Map(
        IncomingSubscriptionMessage message,
        string data)
    {
        Initialize();

        var eventType = message.Subscription.Type;

        var type = _eventTypes!.GetValueOrDefault(eventType);

        return type is null ? null : Deserialize(type, data);
    }

    private WebhookBaseEvent? Deserialize(Type type, string data)
    {
        var method = typeof(EventMapper)
            .GetMethod(nameof(DeserializeFromWrapper),
                BindingFlags.NonPublic | BindingFlags.Instance);

        var generic = method!.MakeGenericMethod(type);

        return (WebhookBaseEvent?)generic.Invoke(this, [data]);
    }

    private TEvent? DeserializeFromWrapper<TEvent>(string data)
        where TEvent : WebhookBaseEvent
    {
        var wrapper = JsonSerializer.Deserialize<Wrapper<TEvent>>(data, Options);

        return wrapper?.Event;
    }

    public class Wrapper<T>
    {
        public T? Event { get; set; }
    }
}