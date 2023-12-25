using System.Reflection;

namespace twitchstreambot.webhooks.Events.Values;

public class SubTier
{
    public static readonly SubTier Tier1 = new("1000");
    public static readonly SubTier Tier2 = new("2000");
    public static readonly SubTier Tier3 = new("3000");
    public static readonly SubTier Unknown = new("unknown");
    public string Tier { get; set; }

    private SubTier(string tier)
    {
        Tier = tier;
    }

    public static implicit operator string(SubTier tier) => tier.Tier;

    public static implicit operator SubTier(string tier)
    {
        var availableTypes = typeof(SubTier)
            .GetFields(BindingFlags.Static | BindingFlags.Public)
            .Where(t => t.FieldType == typeof(SubTier))
            .Select(t => t.GetValue(null) as SubTier)
            .SingleOrDefault(t => t?.Tier == tier);

        return availableTypes ?? Unknown;
    }
}