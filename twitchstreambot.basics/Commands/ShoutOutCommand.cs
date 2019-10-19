using System.Collections.Generic;
using System.Linq;
using twitchstreambot.infrastructure;
using twitchstreambot.Parsing;

namespace twitchstreambot.Commands
{
    [TwitchCommand("so", IsPrivate = true)]
    public class ShoutOutCommand : ITwitchCommand
    {
        public bool CanExecute(TwitchMessage message)
        {
            if (message.Headers.ContainsKey("badges"))
            {
                var badges = message.Headers["badges"].ToLower();

                return badges.Contains("broadcaster") ||
                       badges.Contains("moderator");
            }

            return false;
        }

        public string Execute(TwitchMessage message)
        {
            if (!message.Command.Arguments.Any())
            {
                return "so <username>";
            }

            string userName = message.Command.Arguments.First().Replace("@", "");
            
            return $"Checkout {userName} at http://twitch.tv/{userName}";
        }
    }
}