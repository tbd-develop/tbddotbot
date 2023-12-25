namespace twitchstreambot.webhooks.Infrastructure;

public class IncomingRequestHeaders
{
    public const string EventSubMessageId = "Twitch-Eventsub-Message-Id";
    public const string EventSubMessageRetry = "Twitch-Eventsub-Message-Retry";
    public const string EventSubMessageType = "Twitch-Eventsub-Message-Type";
    public const string EventSubMessageSignature = "Twitch-Eventsub-Message-Signature";
    public const string EventSubMessageTimestamp = "Twitch-Eventsub-Message-Timestamp";
    public const string EventSubSubscriptionType = "Twitch-Eventsub-Subscription-Type";
    public const string EventSubSubscriptionVersion = "Twitch-Eventsub-Subscription-Version";
}