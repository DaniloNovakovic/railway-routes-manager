using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Server.Core
{
    public interface ILogicalRepository<TEntity> : IRepository<TEntity> where TEntity : class, ILogical
    {
        TEntity GetDeleted(params object[] keyValues);

        TEntity GetDeleted(Expression<Func<TEntity, bool>> filter);

        IEnumerable<TEntity> GetAllDeleted(Expression<Func<TEntity, bool>> filter = null);

        bool Resurrect(params object[] keyValues);
    }
}