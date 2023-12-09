using System;
using Newtonsoft.Json;

namespace twitchstreambot.Models
{
    public class StreamMarker
    {
        public string Id { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
        [JsonProperty("position_seconds")]
        public string PositionSeconds { get; set; }
        public string Url { get; set; }
    }
}