using Newtonsoft.Json;

namespace twitchstreambot.pubsub.Models
{
    public class PubSubResponseMessage
    {
        public string Type { get; set; }
        public MessageBody Data { get; set; }

        public class MessageBody
        {
            public string Topic { get; set; }
            public string Message { get; set; }
        }
    }

    public class PubSubResponseMessage<T>
    {
        public string Type { get; set; }
        public MessageBody Data { get; set; }

        public class MessageBody
        {
            public T Message { get; set; }
        }
    }

}
