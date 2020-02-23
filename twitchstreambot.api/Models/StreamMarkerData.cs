using System.Collections.Generic;
using Newtonsoft.Json;

namespace twitchstreambot.api.Models
{
    public class StreamMarkerData
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("user_name")]
        public string UserName { get; set; }
        public IEnumerable<StreamVideo> Videos { get; set; }
    }
}