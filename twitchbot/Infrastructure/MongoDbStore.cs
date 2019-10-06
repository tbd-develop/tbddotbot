using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using MongoDB.Driver;

namespace twitchbot.Infrastructure
{
    public class MongoDbStore : IStorage
    {
        private readonly IMongoDatabase _database;

        public MongoDbStore()
        {
            var client = new MongoClient(new MongoUrl("mongodb://localhost:27017"));

            _database = client.GetDatabase("streamgames");
        }

        public void Store<T>(T entity)
        {
            var collection = _database.GetCollection<T>(typeof(T).Name);

            collection.InsertOne(entity);
        }

        public IEnumerable<T> Query<T>(Expression<Func<T, bool>> query)
        {
            var collection = _database.GetCollection<T>(typeof(T).Name);

            return collection.Find(query).ToList();
        }

        public void Update<T>(T entity)
        {
            var filter = GetFilter(entity);

            var collection = _database.GetCollection<T>(typeof(T).Name);

            collection.ReplaceOne(filter, entity);
        }

        private Expression<Func<T, bool>> GetFilter<T>(T entity)
        {
            var properties = from p in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                             where p.GetCustomAttributes(typeof(KeyAttribute)).Any()
                             select p;

            var param = Expression.Parameter(typeof(T), "x");

            Expression<Func<T, bool>> result = null;

            foreach (var property in properties)
            {
                var memberExpression = Expression.PropertyOrField(Expression.Constant(entity), property.Name);

                var lambda = Expression.Lambda<Func<T, bool>>(
                    Expression.Equal(memberExpression, Expression.Constant(property.GetValue(entity))), param);

                result = result == null
                    ? lambda
                    : Expression.Lambda<Func<T, bool>>(Expression.And(result.Body, lambda.Body), param);
            }

            return result;
        }
    }
}