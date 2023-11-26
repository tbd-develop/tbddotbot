using System.Collections.Generic;
using Newtonsoft.Json;

namespace twitchstreambot.Models;

public class TwitchTokenResponse
{
    [JsonProperty("access_token")] public string AccessToken { get; set; } = null!;
    [JsonProperty("expires_in")] public int ExpiresIn { get; set; }
    public IEnumerable<string> Scope { get; set; } = null!;
    [JsonProperty("token_type")] public string TokenType { get; set; } = null!;
}