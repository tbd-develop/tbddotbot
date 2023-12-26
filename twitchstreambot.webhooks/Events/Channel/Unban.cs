using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Events.Contracts;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel;

[WebhookEvent("channel.unban")]
public class Unban : WebhookFromBroadcasterEvent, IContainUserInformation, IContainModeratorInformation
{
    [JsonPropertyName("user_id")] public string? UserId { get; set; }
    [JsonPropertyName("user_name")] public string? UserName { get; set; }
    [JsonPropertyName("user_login")] public string? UserLogin { get; set; }


    [JsonPropertyName("moderator_user_id")]
    public string? ModeratorUserId { get; set; }

    [JsonPropertyName("moderator_user_name")]
    public string? ModeratorUserName { get; set; }

    [JsonPropertyName("moderator_user_login")]
    public string? ModeratorUserLogin { get; set; }
}