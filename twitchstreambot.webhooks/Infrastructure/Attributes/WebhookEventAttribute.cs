namespace twitchstreambot.webhooks.Infrastructure.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class WebhookEventAttribute(string eventName, int version = 1) : Attribute
{
    public string Event { get; set; } = eventName;
    public int Version { get; set; } = version;
}