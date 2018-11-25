using System;
using twitchbot.infrastructure;

namespace twitchbot.commands
{
    [TwitchCommand("christmas")]
    public class ChristmasCommand : ITwitchCommand
    {
        public string Execute(params string[] args)
        {
            var now = DateTime.UtcNow;

            var duration = new DateTime(now.Year, 12, 25, 0, 0, 0).Subtract(now);

            if (duration.Days > 30)
            {
                return $"{duration.Days:##0} days until Christmas";
            }

            return
                $"{duration.Days:#0} days {duration.Hours} hours {duration.Minutes} minutes until Christmas {now.Year}";
        }
    }
}