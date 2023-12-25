using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace twitchstreambot.Models
{
    public class ValidationResponse
    {
        [JsonPropertyName("client_id")] public string ClientId { get; set; } = null!;
        public string Login { get; set; } = null!;
        public IEnumerable<string> Scopes { get; set; } = null!;
        
        [JsonPropertyName("user_id")] public string UserId { get; set; } = null!;
        [JsonPropertyName("expires_in")] public int Expires { get; set; }
    }
}