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

    public class PubSubWhisperResponse
    {
        public string Topic { get; set; }
        public WhisperData Message { get; set; }

        public class WhisperData
        {
            public string Type { get; set; }
        }

        /*
         * {
  "type":"MESSAGE",
  "data":{
     "topic":"whispers.44322889",
     "message":{
        "type":"whisper_received",
        "data":{
           "id":41
        },
         */

        //public string Type { get; set; }
        //public DataLink Data { get; set; }
        //[JsonProperty("thread_id")] public string ThreadId { get; set; }
        //public string Body { get; set; }
        //[JsonProperty("sent_ts")] public long SentTimestamp { get; set; }
        //[JsonProperty("from_id")] public long FromId { get; set; }

        //public class DataLink
        //{
        //    public int Id { get; set; }
        //}
    }



}
