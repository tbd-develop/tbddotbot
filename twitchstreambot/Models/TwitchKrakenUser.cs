using System;
using Newtonsoft.Json;

namespace twitchstreambot.Models
{
    public class TwitchKrakenUser
    {
        [JsonProperty("_id")]
        public long Id { get; set; }
        public string Bio { get; set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public bool Partnered { get; set; }
        [JsonProperty("twitter_connected")]
        public bool TwitterConnected { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}