using System;
using Newtonsoft.Json;

namespace twitchstreambot.models
{
    public class TwitchData
    {
        public StreamData[] Data { get; set; }

        public class StreamData
        {
            public long Id { get; set; }
            public string Title { get; set; }
            [JsonProperty("viewer_count")]
            public int Viewers { get; set; }
            [JsonProperty("started_at")] 
            public DateTime StartedAt { get; set; }    
        }
    }
}