using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Events.Contracts;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.User;

[WebhookEvent("user.update")]
public class Update : WebhookBaseEvent, IContainUserInformation
{
    [JsonPropertyName("user_id")] public string? UserId { get; set; }

    [JsonPropertyName("user_name")] public string? UserName { get; set; }

    [JsonPropertyName("user_login")] public string? UserLogin { get; set; }

    public string? Email { get; set; }

    [JsonPropertyName("email_verified")] public bool IsEmailVerified { get; set; }

    public string Description { get; set; } = null!;
}