namespace twitchstreambot.pubsub.Messaging
{
    public class PubSubListenResponse
    {
        public string Type { get; set; }
        public string Error { get; set; }

        public bool IsError => !string.IsNullOrEmpty(Error);
    }
}