namespace twitchstreambot.infrastructure
{
    public class TwitchConnection
    {
        public string HostName { get; set; }
        public int Port { get; set; }
        public string Channel { get; set; }
        public string BotName { get; set; }
    }
}