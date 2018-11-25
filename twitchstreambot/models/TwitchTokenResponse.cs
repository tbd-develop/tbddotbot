using System.Collections.Generic;
using Newtonsoft.Json;

namespace twitchstreambot.models
{
    public class TwitchTokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("expires_in")]
        public int ExpiresIn {get;set;}
        public IEnumerable<string> Scope { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
    }
}