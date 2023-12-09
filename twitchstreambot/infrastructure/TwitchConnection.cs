namespace twitchstreambot.Infrastructure
{
    public class TwitchConnection
    {
        public string Name { get; set; } = null!;
        public string HostName { get; set; } = null!;
        public int Port { get; set; }
        public string Channel { get; set; } = null!;
        public string Welcome { get; set; } = null!;
    }
}