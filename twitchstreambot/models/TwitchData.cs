using System;
using Newtonsoft.Json;

namespace twitchstreambot.models
{
    public class TwitchData
    {
        public StreamData Stream { get; set; }

        public class StreamData
        {
            [JsonProperty("_id")] public long Id { get; set; }
            public string Game { get; set; }
            public int Viewers { get; set; }
            [JsonProperty("created_at")] public DateTime CreatedAt { get; set; }    
        }
    }
}