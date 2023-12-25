using Microsoft.AspNetCore.Http;

namespace twitchstreambot.webhooks.Infrastructure;

public class TwitchHeaderCollection
{
    public string? MessageId { get; set; } = null!;
    public string? MessageRetry { get; set; } = null!;
    public IncomingMessageType MessageType { get; set; } = null!;
    public string? MessageSignature { get; set; } = null!;

    public DateTime? MessageTimestamp => DateTime.TryParse(MessageTimestampString, out var result)
        ? result
        : null;

    public string? MessageTimestampString { get; set; } = null!;
    public string? SubscriptionType { get; set; } = null!;
    public string? SubscriptionVersion { get; set; } = null!;

    public bool IsValid =>
        !string.IsNullOrEmpty(MessageId) &&
        !string.IsNullOrEmpty(MessageSignature) &&
        MessageTimestamp is not null &&
        MessageType != IncomingMessageType.Unknown;

    public static TwitchHeaderCollection FromHeaders(IHeaderDictionary headers)
    {
        return new TwitchHeaderCollection
        {
            MessageId = headers[IncomingRequestHeaders.EventSubMessageId].SingleOrDefault(),
            MessageRetry = headers[IncomingRequestHeaders.EventSubMessageRetry].SingleOrDefault(),
            MessageType = headers[IncomingRequestHeaders.EventSubMessageType].SingleOrDefault() ??
                          IncomingMessageType.Unknown,
            MessageSignature = headers[IncomingRequestHeaders.EventSubMessageSignature].SingleOrDefault(),
            MessageTimestampString = headers[IncomingRequestHeaders.EventSubMessageTimestamp].SingleOrDefault(),
            SubscriptionType = headers[IncomingRequestHeaders.EventSubSubscriptionType].SingleOrDefault(),
            SubscriptionVersion = headers[IncomingRequestHeaders.EventSubSubscriptionVersion].SingleOrDefault()
        };
    }
}