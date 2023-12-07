using System.Linq;
using twitchstreambot.Parsing;

namespace twitchstreambot.Infrastructure.Extensions;

public static class CommandActionExtensions
{
    public static bool IsInRole(this TwitchMessage message, params StreamRole[] roles)
    {
        string? header = null;

        if (message.Headers is not null &&
            !message.Headers.TryGetValue("badges", out header))
            return false;

        if (header is null) return false;
        
        var badges = header.ToLower();

        return roles.Any(r => badges.Contains(r));
    }
}