using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace twitchstreambot.Models;

public class TwitchTokenResponse
{
    [JsonPropertyName("access_token")] public string AccessToken { get; set; } = null!;
    [JsonPropertyName("expires_in")] public int ExpiresIn { get; set; }
    public IEnumerable<string> Scope { get; set; } = null!;
    [JsonPropertyName("token_type")] public string TokenType { get; set; } = null!;
}