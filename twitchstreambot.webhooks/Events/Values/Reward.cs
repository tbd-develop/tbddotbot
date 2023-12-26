namespace twitchstreambot.webhooks.Events.Values;

public class Reward
{
    public string? Id { get; set; } = null!;
    public string? Title { get; set; } = null!;
    public int Cost { get; set; }
    public string? Prompt { get; set; } = null!;
}