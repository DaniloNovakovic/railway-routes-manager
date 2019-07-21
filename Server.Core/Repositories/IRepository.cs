using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Server.Core
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        TEntity Get(params object[] keyValues);

        IEnumerable<TEntity> GetAll();

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);
    }
}