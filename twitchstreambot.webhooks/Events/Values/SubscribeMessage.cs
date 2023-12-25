namespace twitchstreambot.webhooks.Events.Values;

public class SubscribeMessage
{
    public string? Text { get; set; }
    public SubscribeEmote[] Emotes { get; set; } = null!;
}