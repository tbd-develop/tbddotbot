using System.Collections.Generic;
using System.Linq;
using twitchstreambot.infrastructure;
using twitchstreambot.Parsing;

namespace twitchstreambot.Commands
{
    [TwitchCommand("so", IsPrivate = true)]
    public class ShoutOutCommand : ITwitchCommand
    {
        private readonly Dictionary<string, string> _headers;

        public ShoutOutCommand(Dictionary<string, string> headers)
        {
            _headers = headers;
        }

        public bool CanExecute(TwitchMessage message)
        {
            if (_headers.ContainsKey("badges"))
            {
                var badges = _headers["badges"].ToLower();

                return badges.Contains("broadcaster") ||
                       badges.Contains("moderator");
            }

            return false;
        }

        public string Execute(TwitchMessage message)
        {
            //if (!args.Any())
            //{
            //    return "so <username>";
            //}

            //string userName = args.First().Replace("@", "");

            //return $"Checkout {userName} at http://twitch.tv/{userName}";
            return string.Empty;
        }
    }
}