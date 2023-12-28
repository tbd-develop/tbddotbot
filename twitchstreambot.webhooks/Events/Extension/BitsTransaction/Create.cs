using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Events.Contracts;
using twitchstreambot.webhooks.Events.Values;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Extension.BitsTransaction;

[WebhookEvent("extension.bits_transaction.create")]
public class Create : WebhookBaseEvent, IContainBroadcasterInformation, IContainUserInformation
{
    public string Id { get; set; } = null!;

    [JsonPropertyName("extension_client_id")]
    public string ExtensionClientId { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; set; } = null!;

    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; set; } = null!;

    [JsonPropertyName("user_id")] public string? UserId { get; set; }

    [JsonPropertyName("user_name")] public string? UserName { get; set; }

    [JsonPropertyName("user_login")] public string? UserLogin { get; set; }

    public BitsExtensionProduct Product { get; set; } = null!;
}