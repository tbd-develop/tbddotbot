using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace twitchbot.Infrastructure
{
    public interface IStorage
    {
        void Store<T>(T entity);
        IEnumerable<T> Query<T>(Expression<Func<T, bool>> query);
        void Update<T>(T entity);
    }
}