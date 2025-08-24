using Microsoft.EntityFrameworkCore;
using PATCHUB.AuthServer.Domain.Common.Primitives;
using PATCHUB.AuthServer.Domain.Repositories.Base;
using PATCHUB.SharedLibrary.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PATCHUB.AuthServer.Persistence.Repositories.Base
{
    public abstract class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
    {
        //protected readonly DbContext _context;
        protected readonly DbContext _context;

        public RepositoryBase(DbContext context)
        {
            _context = context;
        }

        public TEntity Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            return entity;
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            return entity;
        }
        public virtual void Update(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(TKey id)
        {
            TEntity entity = _context.Set<TEntity>().Find(id);
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            var dbSet = _context.Set<TEntity>();
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public virtual void DeleteAll(IEnumerable<TEntity> entities)
        {
            var dbSet = _context.Set<TEntity>();

            foreach (var entity in entities)
            {
                if (_context.Entry(entity).State == EntityState.Detached)
                {
                    dbSet.Remove(entity);
                }
                dbSet.Remove(entity);
            }

        }

        protected virtual IQueryable<TEntity> GetQueryable(
          Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          string includeProperties = null,
          int? skip = null,
          int? take = null)

        {
            try
            {
                // _context.ChangeTracker.AutoDetectChangesEnabled = false;

                includeProperties = includeProperties ?? string.Empty;
                IQueryable<TEntity> query = _context.Set<TEntity>();

                query = query.AsNoTracking();


                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (orderBy != null)
                {
                    query = orderBy(query);
                }

                if (skip.HasValue)
                {
                    query = query.Skip(skip.Value);
                }

                if (take.HasValue)
                {
                    query = query.Take(take.Value);
                }

                return query;
            }
            finally
            {
                _context.ChangeTracker.AutoDetectChangesEnabled = true;
            }
        }

        public virtual IEnumerable<TEntity> GetAll(
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          string includeProperties = null,
          int? skip = null,
          int? take = null)

        {
            return GetQueryable(
                filter: null,
                orderBy: orderBy,
                includeProperties: includeProperties,
                skip: skip,
                take: take)
              .ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          string includeProperties = null,
          int? skip = null,
          int? take = null)

        {
            return await GetQueryable(
                filter: null,
                orderBy: orderBy,
                includeProperties: includeProperties,
                skip: skip,
                take: take)
              .ToListAsync();
        }

        public virtual IEnumerable<TEntity> Get(
          Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          string includeProperties = null,
          int? skip = null)

        {
            return GetQueryable(
                filter: filter,
                orderBy: orderBy,
                includeProperties: includeProperties,
                skip: skip)
              .ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(
          Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          string includeProperties = null,
          int? skip = null,
          int? take = null)

        {
            return await GetQueryable(
                filter: filter,
                orderBy: orderBy,
                includeProperties: includeProperties,
                skip: skip,
                take: take)
              .ToListAsync();
        }

        public virtual TEntity GetOne(
          Expression<Func<TEntity, bool>> filter = null,
          string includeProperties = "")

        {
            return GetQueryable(
                filter: filter,
                orderBy: null,
                includeProperties: includeProperties)
              .SingleOrDefault();
        }

        public virtual async Task<TEntity> GetOneAsync(
          Expression<Func<TEntity, bool>> filter = null,
          string includeProperties = null)

        {
            return await GetQueryable(
                filter: filter,
                orderBy: null,
                includeProperties: includeProperties)
              .SingleOrDefaultAsync();
        }

        public virtual TEntity GetFirst(
          Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          string includeProperties = null)

        {
            return GetQueryable(
                filter: filter,
                orderBy: orderBy,
                includeProperties: includeProperties)
              .FirstOrDefault();
        }

        public virtual async Task<TEntity> GetFirstAsync(
          Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          string includeProperties = null)

        {
            return await GetQueryable(
                filter: filter,
                orderBy: orderBy,
                includeProperties: includeProperties)
              .FirstOrDefaultAsync();
        }

        public virtual TEntity GetById(
          TKey id)

        {
            TEntity query = null;
            query = _context.Set<TEntity>().Find(id);

            return query;
        }

        public virtual async Task<TEntity> GetByIdAsync(
          TKey id)

        {
            TEntity query = null;
            query = await _context.Set<TEntity>().FindAsync(id);

            return query;
        }

        public virtual int GetCount(
          Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(
                filter: filter)
              .Count();
        }

        public virtual async Task<int> GetCountAsync(
          Expression<Func<TEntity, bool>> filter = null)

        {
            return await GetQueryable(
                filter: filter)
              .CountAsync();
        }

        public virtual bool GetExists(
          Expression<Func<TEntity, bool>> filter = null)

        {
            return GetQueryable(
                filter: filter)
              .Any();
        }

        public virtual async Task<bool> GetExistsAsync(
          Expression<Func<TEntity, bool>> filter = null)

        {
            return await GetQueryable(
                filter: filter)
              .AnyAsync();
        }
    }

}
