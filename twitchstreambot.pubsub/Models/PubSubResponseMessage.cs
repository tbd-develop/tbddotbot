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

            [JsonProperty("data_object")]
            public string DataObject { get; set; }
        }
    }

    public class PubSubWhisperResponse
    {
        public string Type { get; set; }
        public DataLink Data { get; set; }
        [JsonProperty("thread_id")]
        public string ThreadId { get; set; }
        public string Body { get; set; }
        [JsonProperty("sent_ts")]
        public long SentTimestamp { get; set; }
        [JsonProperty("from_id")]
        public long FromId { get; set; }

        public class DataLink
        {
            public int Id { get; set; }
        }
    }
}