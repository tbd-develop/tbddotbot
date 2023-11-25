using System.Linq;
using twitchstreambot.Parsing;

namespace twitchstreambot.Infrastructure.Extensions
{
    public static class CommandActionExtensions
    {
        public static bool IsInRole(this TwitchMessage message, params StreamRole[] roles)
        {
            if (!message.Headers.TryGetValue("badges", out var header)) return false;
            
            var badges = header.ToLower();

            return roles.Any(r => badges.Contains(r));
        }
    }
}