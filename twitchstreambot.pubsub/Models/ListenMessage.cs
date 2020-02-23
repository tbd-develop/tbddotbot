using Newtonsoft.Json;

namespace twitchstreambot.pubsub.Models
{
    public class ListenMessage
    {
        public string Type => "LISTEN";
        public ListenData Data { get; set; }

        public class ListenData
        {
            public string[] Topics { get; set; }
            [JsonProperty("auth_token")]
            public string AuthToken { get; set; }
        }
    }
}