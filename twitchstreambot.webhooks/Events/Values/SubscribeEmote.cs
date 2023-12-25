namespace twitchstreambot.webhooks.Events.Values;

public class SubscribeEmote
{
    public int Begin { get; set; }
    public int End { get; set; }
    public string Id { get; set; } = null!;
}