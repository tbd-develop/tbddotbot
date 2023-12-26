﻿using System.Text.Json.Serialization;
using twitchstreambot.Infrastructure;
using twitchstreambot.webhooks.Infrastructure;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel.Chat;

[WebhookEvent("channel.chat.clear_user_messages")]
public class ClearUserMessagesEvent : WebhookFromBroadcasterEvent
{
    [JsonPropertyName("target_user_id")] public string TargetUserId { get; set; } = null!;

    [JsonPropertyName("target_user_login")]
    public string TargetUserLogin { get; set; } = null!;

    [JsonPropertyName("target_user_name")] public string TargetUserName { get; set; } = null!;
}