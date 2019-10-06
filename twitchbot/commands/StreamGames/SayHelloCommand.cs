using System.Collections.Generic;
using System.Threading.Tasks;
using twitchbot.Infrastructure;
using twitchstreambot.infrastructure;

namespace twitchbot.commands.StreamGames
{
    [TwitchCommand("hello")]
    public class SayHelloCommand : ITwitchCommand
    {
        private readonly Dictionary<string, string> _headers;
        private readonly SignalRClient _client;

        public SayHelloCommand(Dictionary<string, string> headers, SignalRClient client)
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
                    await _client.Connection.SendCoreAsync("Echo", new object[] { string.Join(' ', args) });
                });

            return string.Empty;
        }
    }
}