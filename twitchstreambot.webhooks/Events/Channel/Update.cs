using System.Text.Json.Serialization;
using twitchstreambot.Infrastructure;
using twitchstreambot.webhooks.Infrastructure;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel;

[WebhookEvent("channel.update", 2)]
public class Update : WebhookBaseEvent
{
    public string Title { get; set; } = null!;

    public string Language { get; set; } = null!;

    [JsonPropertyName("category_id")] public string CategoryId { get; set; } = null!;

    [JsonPropertyName("category_name")] public string CategoryName { get; set; } = null!;

    [JsonPropertyName("content_classification_labels")]
    public string[] ContentClassificationLabels { get; set; } = null!;
}