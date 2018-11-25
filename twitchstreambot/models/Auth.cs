using System;

namespace twitchstreambot.models
{
    public class Auth
    {
        public string AuthToken { get; set; }
        public string ClientId { get; set; }
        public string Secret { get; set; }
        public string Scope { get; set; }
        public DateTime? Expiration { get; set; }
    }
}