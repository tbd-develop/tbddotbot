using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public void Update<T>(T entity)
        {
            using (var database = new LiteDatabase(_connectionString))
            {
                var collection = database.GetCollection<T>();

                collection.Update(entity);
            }
        }

        public IEnumerable<T> Query<T>(Expression<Func<T, bool>> query)
        {
            using (var database = new LiteDatabase(_connectionString))
            {
                var collection = database.GetCollection<T>();

                var results = collection.Find(query).ToList();

                return results;
            }
        }
    }
}