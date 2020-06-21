using Newtonsoft.Json;

namespace twitchstreambot.api.Models
{
    public class ChannelResponse
    {
        [JsonProperty("mature")]
        public bool IsMature { get; set; }
        [JsonProperty("_id")]
        public string Id { get; set; }
        public int Views { get; set; }
        public int Followers { get; set; }
    }
}