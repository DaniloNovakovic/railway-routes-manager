using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Server.Core
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        TEntity Get(params object[] keyValues);

        TEntity Get(Expression<Func<TEntity, bool>> filter);

        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null);

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);
    }
}