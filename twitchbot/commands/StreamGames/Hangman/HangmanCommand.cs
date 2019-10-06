using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using twitchbot.Infrastructure;
using twitchstreambot.infrastructure;

namespace twitchbot.commands.StreamGames.Hangman
{
    [TwitchCommand("hangman")]
    public class HangmanCommand : ITwitchCommand
    {
        private readonly Dictionary<string, string> _headers;
        private readonly SignalRClient _client;

        public HangmanCommand(Dictionary<string, string> headers, SignalRClient client)
        {
            _headers = headers;
            _client = client;
        }

        public bool CanExecute()
        {
            return true;
        }

        public string Execute(params string[] args)
        {
            if (!args.Any() || !IsValidArgument(args[0]))
            {
                return "Requires Arguments";
            }

            Task.Run(async () =>
            {
                await _client.Connection.SendCoreAsync("Status", new object[]
                {
                    new TwitchChatCommand()
                    {
                        UserName = _headers["display-name"],
                        Value = args[0]
                    }
                });
            });

            return string.Empty;
        }

        private bool IsValidArgument(string argument)
        {
            return new[] { "reset" }.Any(x => x == argument);
        }
    }
}