using System.Collections.Generic;
using System.Threading.Tasks;
using twitchbot.Infrastructure;
using twitchstreambot.infrastructure;

namespace twitchbot.commands.StreamGames
{
    [TwitchCommand("addnumber")]
    public class AddNumberCommand : ITwitchCommand
    {
        private readonly Dictionary<string, string> _headers;
        private readonly SignalRClient _client;

        public AddNumberCommand(Dictionary<string, string> headers, SignalRClient client)
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
            Task.Run(async () =>
            {
                await _client.Connection.SendCoreAsync("AddNumber", new object[]
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
    }
}