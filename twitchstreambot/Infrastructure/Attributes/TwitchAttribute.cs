namespace twitchstreambot.Infrastructure.Attributes
{
    public class TwitchAttribute
    {
        public string Element { get; set; } = null!;
        public string? Arguments { get; set; }
    }
}