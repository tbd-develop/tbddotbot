namespace twitchstreambot.pubsub.Models
{
    public class PubSubMessageBase
    {
        public string Type { get; set; }
    }

    public class PubSubMessageBase<T> : PubSubMessageBase
    {
        public T Data { get; set; }
    }
}