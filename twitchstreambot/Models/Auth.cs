using System;

namespace twitchstreambot.Models;

public class Auth
{
    public string AuthToken { get; set; } = null!;
    public string ClientId { get; set; } = null!;
    public string Secret { get; set; } = null!;
    public string Scope { get; set; } = null!;
    public DateTime? Expiration { get; set; }
}