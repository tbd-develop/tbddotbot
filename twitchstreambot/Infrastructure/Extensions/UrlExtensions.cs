using System.Collections.Generic;
using System.Linq;

namespace twitchstreambot.Infrastructure.Extensions;

public static class UrlExtensions
{
    public static string AsQueryParameter(this IEnumerable<string> source, string key, char separator = '&')
    {
        var parameters = source.ToList();

        var results = (from id in parameters
            select $"{key}={id}").ToList();

        return string.Join(separator, results);
    }
}