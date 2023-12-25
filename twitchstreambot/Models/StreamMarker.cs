using System;
using System.Text.Json.Serialization;

namespace twitchstreambot.Models
{
    public class StreamMarker
    {
        public string Id { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
        [JsonPropertyName("position_seconds")]
        public string PositionSeconds { get; set; }
        public string Url { get; set; }
    }
}