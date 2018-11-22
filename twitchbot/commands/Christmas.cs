using System;
using twitchbot.infrastructure;

namespace twitchbot.commands
{
    public class Christmas : ITwitchCommand
    {
        public string Execute(params string[] args)
        {
            var now = DateTime.UtcNow;

            return $"{new DateTime(now.Year, 12, 25).Subtract(now).TotalDays:##0} days until Christmas";
        }
    }
}