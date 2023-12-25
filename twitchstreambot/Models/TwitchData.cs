using System;
using System.Text.Json.Serialization;

namespace twitchstreambot.Models;

public class TwitchData
{
    public StreamData[] Data { get; set; } = null!;

    public class StreamData
    {
        public long Id { get; set; }
        public string Title { get; set; } = null!;
            
        [JsonPropertyName("viewer_count")] public int Viewers { get; set; }
        [JsonPropertyName("started_at")] public DateTime StartedAt { get; set; }
    }
}