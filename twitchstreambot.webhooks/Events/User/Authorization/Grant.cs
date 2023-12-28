using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Events.Contracts;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.User.Authorization;

[WebhookEvent("user.authorization.grant")]
public class Grant : WebhookBaseEvent, IContainUserInformation
{
    [JsonPropertyName("client_id")] public string ClientId { get; set; } = null!;

    [JsonPropertyName("user_id")] public string? UserId { get; set; }

    [JsonPropertyName("user_name")] public string? UserName { get; set; }

    [JsonPropertyName("user_login")] public string? UserLogin { get; set; }
}