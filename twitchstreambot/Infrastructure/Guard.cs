using System;
using System.Collections.Generic;
using System.Linq;

namespace twitchstreambot.Infrastructure
{
    public class Guard
    {
        public static void IsNotEmpty<T>(IEnumerable<T> source, string message)
        {
            if (source == null || !source.Any())
            {
                throw new ArgumentException(message);
            }
        }
    }
}