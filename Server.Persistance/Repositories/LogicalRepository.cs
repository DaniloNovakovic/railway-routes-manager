using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Server.Core;

namespace Server.Persistance
{
    public class LogicalRepository<TEntity> : ILogicalRepository<TEntity> where TEntity : class, ILogical
    {
        private readonly DbContext _context;

        public LogicalRepository(DbContext context)
        {
            _context = context;
        }

        public virtual TEntity Add(TEntity entity)
        {
            return _context.Set<TEntity>().Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        public virtual TEntity Get(params object[] keyValues)
        {
            var entity = _context.Set<TEntity>().Find(keyValues);
            return entity?.DeletionDate == null ? entity : null;
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return GetEntities().FirstOrDefault(filter);
        }

        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetEntities().Where(filter).ToList();
        }

        public virtual void Remove(TEntity entity)
        {
            entity.DeletionDate = DateTime.Now;
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.DeletionDate = DateTime.Now;
            }
        }

        public virtual void Resurrect(params object[] keyValues)
        {
            var entity = _context.Set<TEntity>().Find(keyValues);

            if (entity != null)
            {
                entity.DeletionDate = null;
            }
        }

        private IQueryable<TEntity> GetEntities()
        {
            return _context.Set<TEntity>().AsQueryable().Where(e => e.DeletionDate == null);
        }
    }
}