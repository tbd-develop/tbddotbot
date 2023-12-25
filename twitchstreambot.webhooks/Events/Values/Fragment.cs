namespace twitchstreambot.webhooks.Events.Values;

public class Fragment
{
    public string Type { get; set; } = null!;
    public string Text { get; set; } = null!;
    public CheerMote? Cheermote { get; set; }
    public Emote? Emote { get; set; }

    public Mention? Mention { get; set; }
}