using System.Collections.Generic;
using System.Linq;
using twitchbot.Infrastructure.Models;

namespace twitchbot.Infrastructure
{
    public class StreamGames
    {
        private readonly IStorage _storage;

        public StreamGames(IStorage storage)
        {
            _storage = storage;
        }

        public IDictionary<string, int> GetTop5(string game)
        {
            var board = _storage.Query<Leaderboard>(l => l.Name == game).SingleOrDefault();

            return board?.Results.Take(5).ToDictionary(k => k.Key, k => k.Value);
        }

        public void MergeResults(string game, Dictionary<string, int> results)
        {
            var board = _storage.Query<Leaderboard>(l => l.Name == game).SingleOrDefault();

            if (board == null)
            {
                _storage.Store(new Leaderboard
                {
                    Name = game,
                    Results = results
                });
            }
            else
            {
                board.Results = results.Concat(board.Results).GroupBy(d => d.Key)
                    .ToDictionary(d => d.Key, d => d.Sum(x => x.Value)).OrderByDescending(k => k.Value)
                    .ToDictionary(k => k.Key, k => k.Value);

                _storage.Update(board);
            }
        }
    }
}