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

        public RegisterCommand(IStorage storage, Dictionary<string, string> headers)
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

            int twitchId = int.Parse(_headers["user-id"]);


            var existing = _storage.Query<ChannelUser>(q => q.TwitchId == twitchId).SingleOrDefault();

            if (existing != null)
            {
                existing.DisplayName = args[0];

                _storage.Update(existing);
            }
            else
            {
                _storage.Store(new ChannelUser()
                {
                    UserName = _headers["display-name"],
                    TwitchId = twitchId,
                    DisplayName = args[0]
                });
            }

            return string.Empty;
        }
    }
}