using System.Reflection;

namespace twitchstreambot.webhooks.Events.Values;

public class NoticeType
{
    public static readonly NoticeType Sub = new("sub");
    public static readonly NoticeType Resub = new("resub");
    public static readonly NoticeType SubGift = new("sub_gift");
    public static readonly NoticeType CommunitySubGift = new("community_sub_gift");
    public static readonly NoticeType GiftPaidUpgrade = new("gift_paid_upgrade");
    public static readonly NoticeType PrimePaidUpgrade = new("prime_paid_upgrade");
    public static readonly NoticeType Raid = new("raid");
    public static readonly NoticeType UnRaid = new("unraid");
    public static readonly NoticeType PayItForward = new("pay_it_forward");
    public static readonly NoticeType Announcement = new("announcement");
    public static readonly NoticeType BitsBadgeTier = new("bits_badge_tier");
    public static readonly NoticeType CharityDonation = new("charity_donation");
    public static readonly NoticeType Unknown = new("unknown");

    private string Type { get; }

    private NoticeType(string type)
    {
        Type = type;
    }

    public static implicit operator string(NoticeType type) => type.Type;

    public static implicit operator NoticeType(string type)
    {
        var availableTypes = typeof(NoticeType)
            .GetFields(BindingFlags.Static | BindingFlags.Public)
            .Where(t => t.FieldType == typeof(NoticeType))
            .Select(t => t.GetValue(null) as NoticeType)
            .SingleOrDefault(t => t?.Type == type);

        return availableTypes ?? Unknown;
    }
}