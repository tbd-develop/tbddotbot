using System.Text.Json.Serialization;
using twitchstreambot.Infrastructure;
using twitchstreambot.webhooks.Events.Values;
using twitchstreambot.webhooks.Infrastructure;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel.Chat;

[WebhookEvent("channel.chat.notification")]
public class Notification : WebhookBaseEvent
{
    [JsonPropertyName("chatter_user_id")] public string ChatterUserId { get; set; } = null!;

    [JsonPropertyName("chatter_user_login")]
    public string ChatterUserLogin { get; set; } = null!;

    [JsonPropertyName("chatter_user_name")]
    public string ChatterUserName { get; set; } = null!;

    [JsonPropertyName("chatter_is_anonymous")]
    public bool ChatterIsAnonymous { get; set; }

    [JsonPropertyName("color")] public string Color { get; set; } = null!;

    [JsonPropertyName("badges")] public Badge[] Badges { get; set; } = null!;

    [JsonPropertyName("system_message")] public string SystemMessage { get; set; } = null!;
    [JsonPropertyName("message_id")] public string MessageId { get; set; } = null!;

    [JsonPropertyName("message")] public Message Message { get; set; } = null!;

    [JsonPropertyName("notice_type")] public NoticeType NoticeType { get; set; } = null!;

    public Sub Sub { get; set; } = null!;
    [JsonPropertyName("sub_gift")] public SubGift SubGift { get; set; } = null!;

    [JsonPropertyName("community_sub_gift")]
    public CommunitySubGift? CommunitySubGift { get; set; }

    [JsonPropertyName("gift_paid_upgrade")]
    public GiftedInformation? GiftPaidUpgrade { get; set; }

    [JsonPropertyName("prime_paid_upgrade")]
    public PrimePaidUpgrade? PrimePaidUpgrade { get; set; }

    [JsonPropertyName("pay_it_forward")] public GiftedInformation? PayItForward { get; set; }

    public Raid? Raid { get; set; } = null!;

    public object? Unraid { get; set; } = null!;

    public Announcement? Announcement { get; set; }

    [JsonPropertyName("bits_badge_tier")] public BitsBadgeTier? BitsBadgeTier { get; set; }

    [JsonPropertyName("charity_donation")] public CharityDonation? CharityDonation { get; set; }
}