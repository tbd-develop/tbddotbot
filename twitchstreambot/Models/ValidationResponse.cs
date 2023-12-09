using System.Collections.Generic;
using Newtonsoft.Json;

namespace twitchstreambot.Models
{
    public class ValidationResponse
    {
        [JsonProperty("client_id")] public string ClientId { get; set; } = null!;
        public string Login { get; set; } = null!;
        public IEnumerable<string> Scopes { get; set; } = null!;
        [JsonProperty("user_id")] public string UserId { get; set; } = null!;
        [JsonProperty("expires_in")] public int Expires { get; set; }
    }
}