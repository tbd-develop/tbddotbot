using System.Collections.Generic;
using Newtonsoft.Json;

namespace twitchstreambot.api.Models
{
    public class ValidationResponse
    {
        [JsonProperty("client_id")]
        public string ClientId { get; set; }
        public string Login { get; set; }
        public IEnumerable<string> Scopes { get; set; }
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("expires_in")]
        public int Expires { get; set; }
    }
}