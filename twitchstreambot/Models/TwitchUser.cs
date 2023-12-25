using System;
using System.Text.Json.Serialization;

namespace twitchstreambot.Models
{
    public class TwitchUser
    {
        public string Id { get; set; } = null!;
        public string Login { get; set; } = null!;

        [JsonPropertyName("display_name")] public string DisplayName { get; set; } = null!;

        public string Type { get; set; } = null!;

        [JsonPropertyName("broadcaster_type")] public string BroadcasterType { get; set; } = null!;
        public string Description { get; set; } = null!;

        [JsonPropertyName("profile_image_url")]
        public string ProfileImageUrl { get; set; } = null!;

        [JsonPropertyName("offline_image_url")]
        public string OfflineImageUrl { get; set; } = null!;

        [JsonPropertyName("view_count")] public int ViewCount { get; set; }
        public string Email { get; set; } = null!;

        [JsonPropertyName("created_at")] public DateTime? CreatedAt { get; set; } = null!;
    }
}