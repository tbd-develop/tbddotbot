using System.Collections.Generic;
using System.Linq;
using twitchstreambot.infrastructure;

namespace twitchstreambot.Commands
{
    [TwitchCommand("so")]
    public class ShoutOutCommand : ITwitchCommand
    {
        private readonly IDictionary<string, string> _headers;

        public ShoutOutCommand(IDictionary<string, string> headers)
        {
            _headers = headers;
        }

        public bool CanExecute()
        {
            if (_headers.ContainsKey("badges"))
            {
                var badges = _headers["badges"].ToLower();

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