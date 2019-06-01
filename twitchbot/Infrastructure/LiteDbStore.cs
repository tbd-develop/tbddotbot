using LiteDB;

namespace twitchbot.Infrastructure
{
    public class LiteDbStore : IStorage
    {
        private readonly string _connectionString;

        public LiteDbStore(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Store<T>(T entity)
        {
            using (var database = new LiteDatabase(_connectionString))
            {
                var collection = database.GetCollection<T>();

                collection.Insert(entity);
            }
        }
    }
}