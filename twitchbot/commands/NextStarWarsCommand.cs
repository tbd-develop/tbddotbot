using System;
using System.Collections.Generic;
using twitchstreambot.infrastructure;

namespace twitchbot.commands
{
    [TwitchCommand("starwars")]
    public class NextStarWarsCommand : ITwitchCommand
    {
        private readonly DateTime _releaseDate = new DateTime(2019, 12, 20);

        public bool CanExecute(IDictionary<string, string> headers)
        {
            return _releaseDate > DateTime.Now;
        }

        public string Execute(params string[] args)
        {
            TimeSpan difference = _releaseDate.Subtract(DateTime.Now);

            return $"There are {difference.Days} days until Star Wars: The Rise Of Skywalker releases!";
        }
    }
}