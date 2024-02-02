using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Events;
using twitchstreambot.webhooks.Infrastructure.Attributes;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace twitchstreambot.webhooks.Infrastructure;

public class EventMapper
{
    private static Dictionary<(string, int), Type>? _eventTypes = null;
    private static bool IsInitialized => _eventTypes != null;

    private static JsonSerializerOptions Options;

    private static void Initialize()
    {
        if (IsInitialized)
        {
            return;
        }

        Options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var converters = Assembly.GetAssembly(typeof(WebhookBaseEvent))?.GetTypes()
            .Where(x => x is { IsClass: true, IsAbstract: false } && x.IsSubclassOf(typeof(JsonConverter)))
            .Select(x => (JsonConverter)Activator.CreateInstance(x)!)
            .ToList();

        if (converters is not null)
        {
            foreach (var converter in converters)
            {
                Options.Converters.Add(converter);
            }
        }

        _eventTypes = (from type in Assembly.GetAssembly(typeof(WebhookBaseEvent))?.GetTypes()
            where type is { IsClass: true, IsAbstract: false } && type.IsSubclassOf(typeof(WebhookBaseEvent))
            let attribute = type.GetCustomAttribute<WebhookEventAttribute>()
            where attribute is not null
            select new
            {
                attribute.Event,
                attribute.Version,
                type
            }).ToDictionary(x => (x.Event, x.Version), x => x.type);
    }


    public WebhookBaseEvent? Map(
        IncomingSubscriptionMessage message,
        string data)
    {
        Initialize();

        var eventType = message.Subscription.Type;

        var type = _eventTypes!.GetValueOrDefault((eventType,
            int.TryParse(message.Subscription.Version, out var version) ? version : 1));

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