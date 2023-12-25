namespace twitchstreambot.webhooks.Events.Values;

public class CheerMote
{
    public string Prefix { get; set; } = null!;
    public int Bits { get; set; }
    public int Tier { get; set; }
}