using System.Linq;
using twitchstreambot.infrastructure;

namespace twitchbot.commands.StreamGames
{
    [TwitchCommand("leaderboard")]
    public class LeaderboardCommand : ITwitchCommand
    {
        private readonly Infrastructure.StreamGames _games;

        public LeaderboardCommand(Infrastructure.StreamGames games)
        {
            _games = games;
        }

        public bool CanExecute()
        {
            return true;
        }

        public string Execute(params string[] args)
        {
            if (args.Any())
            {
                var game = args[0];

                var results = _games.GetTop5(game);

                var sorted = string.Join(", ", results.OrderByDescending(d => d.Value)
                    .Select(s => $"{s.Key} has {s.Value}").ToArray());

                return sorted;
            }

            return "!leaderboard <gamename> (hangman)";
        }
    }
}