using System.Collections.Generic;
using System.Linq;
using twitchstreambot.infrastructure;

namespace twitchstreambot.commands
{
    [TwitchCommand("so")]
    public class ShoutOutCommand : ITwitchCommand
    {
        public bool CanExecute(IDictionary<string, string> headers)
        {
            if (headers.ContainsKey("badges"))
            {
                var badges = headers["badges"].ToLower();

                return badges.Contains("broadcaster") ||
                       badges.Contains("moderator");
            }

            return false;
        }

        public string Execute(params string[] args)
        {
            if (!args.Any())
            {
                return "so <username>";
            }

            return $"Checkout {args.First()} at http://twitch.tv/{args.First()}";
        }
    }
}