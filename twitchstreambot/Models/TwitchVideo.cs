using System;
using Newtonsoft.Json;

namespace twitchstreambot.Models
{
    public class TwitchVideo
    {
        public string Id { get; set; }
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("user_name")]
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("published_at")]
        public DateTime PublishedAt { get; set; }
        public string Url { get; set; }
        [JsonProperty("thumbnail_url")]
        public string ThumbnailUrl { get; set; }
        public string Viewable { get; set; }
        [JsonProperty("view_count")]
        public int ViewCount { get; set; }
        public string Language { get; set; }
        public string Type { get; set; }
        public string Duration { get; set; }
    }
}