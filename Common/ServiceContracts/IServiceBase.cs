using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.ServiceModel;

namespace Common
{
    [ServiceContract]
    public interface IServiceBase<TKey, TEntity> where TEntity : class
    {
        [OperationContract]
        void Add(TEntity entity);

        [OperationContract]
        TEntity Get(TKey key);

        [OperationContract]
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null);

        [OperationContract]
        void Remove(TKey key);

        [OperationContract]
        void Update(TKey key, TEntity entity);
    }
}