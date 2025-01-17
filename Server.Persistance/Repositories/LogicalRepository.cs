﻿using System;
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

        public virtual IEnumerable<TEntity> GetAllDeleted(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetDeletedEntities().Where(filter).ToList();
        }

        public virtual TEntity GetDeleted(params object[] keyValues)
        {
            var entity = _context.Set<TEntity>().Find(keyValues);
            return entity?.DeletionDate != null ? entity : null;
        }

        public virtual TEntity GetDeleted(Expression<Func<TEntity, bool>> filter)
        {
            return GetDeletedEntities().FirstOrDefault(filter);
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

        public virtual bool Resurrect(params object[] keyValues)
        {
            var entity = _context.Set<TEntity>().Find(keyValues);

            bool shouldResurrect = entity?.DeletionDate != null;

            if (shouldResurrect)
            {
                entity.DeletionDate = null;
            }

            return shouldResurrect;
        }

        private IQueryable<TEntity> GetDeletedEntities()
        {
            return _context.Set<TEntity>().AsQueryable().Where(e => e.DeletionDate != null);
        }

        private IQueryable<TEntity> GetEntities()
        {
            return _context.Set<TEntity>().AsQueryable().Where(e => e.DeletionDate == null);
        }
    }
}