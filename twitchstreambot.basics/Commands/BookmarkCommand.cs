using System.Threading.Tasks;
using twitchstreambot.api;
using twitchstreambot.Infrastructure;
using twitchstreambot.Infrastructure.Attributes;
using twitchstreambot.Parsing;

namespace twitchstreambot.basics.Commands
{
    [TwitchCommand("bookmark")]
    public class BookmarkCommand : ITwitchCommand
    {
        private readonly TwitchHelix _helix;
        private readonly TwitchConnection _connection;

        public BookmarkCommand(TwitchHelix helix, TwitchConnection connection)
        {
            _helix = helix;
            _connection = connection;
        }

        public bool CanExecute(TwitchMessage message)
        {
            return true;
        }

        public string Execute(TwitchMessage message)
        {
            Task.Run(async () =>
            {
                await _helix.CreateStreamMarkerForUser(_connection.Channel,
                    $"{string.Join(" ", message.Command.Arguments)}");
            });

            return "Bookmark captured";
        }
    }
}