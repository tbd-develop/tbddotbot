using System;
using System.Text.Json.Serialization;

namespace twitchstreambot.Models
{
    public class TwitchVideo
    {
        public string Id { get; set; }
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }
        [JsonPropertyName("user_name")]
        public string UserName { get; set; }

        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("published_at")]
        public DateTime PublishedAt { get; set; }
        public string Url { get; set; }
        [JsonPropertyName("thumbnail_url")]
        public string ThumbnailUrl { get; set; }
        public string Viewable { get; set; }
        [JsonPropertyName("view_count")]
        public int ViewCount { get; set; }
        public string Language { get; set; }
        public string Type { get; set; }
        public string Duration { get; set; }
    }
}