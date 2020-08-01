using System.Linq;
using twitchstreambot.Parsing;

namespace twitchstreambot.basics.Infrastructure
{
    public static class CommandActionExtensions
    {
        public static bool IsInRole(this TwitchMessage message, params StreamRole[] roles)
        {
            if (message.Headers.ContainsKey("badges"))
            {
                var badges = message.Headers["badges"].ToLower();

                return roles.Any(r => badges.Contains((string) r));
            }

            return false;
        }
    }
}