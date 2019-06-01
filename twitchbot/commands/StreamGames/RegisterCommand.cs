using System.Collections.Generic;
using System.Linq;
using twitchbot.Infrastructure;
using twitchbot.Infrastructure.Models;
using twitchstreambot.infrastructure;
using twitchstreambot.Parsing;

namespace twitchbot.commands.StreamGames
{
    [TwitchCommand("register")]
    public class RegisterCommand : ITwitchCommand
    {
        private readonly IStorage _storage;
        private readonly IDictionary<string, string> _headers;

        public RegisterCommand(IStorage storage, Dictionary<string,string> headers)
        {
            _storage = storage;
            _headers = headers;
        }

        public bool CanExecute()
        {
            return true;
        }

        public string Execute(params string[] args)
        {
            if (!args.Any())
            {
                return "register <name>";
            }

            //_storage.Store(new ChannelUser() { UserName = });

            return string.Empty;
        }
    }
}