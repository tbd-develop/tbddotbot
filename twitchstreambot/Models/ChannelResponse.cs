using System.Text.Json.Serialization;

namespace twitchstreambot.Models
{
    public class ChannelResponse
    {
        [JsonPropertyName("mature")]
        public bool IsMature { get; set; }
        [JsonPropertyName("_id")]
        public string Id { get; set; }
        public int Views { get; set; }
        public int Followers { get; set; }
    }
}