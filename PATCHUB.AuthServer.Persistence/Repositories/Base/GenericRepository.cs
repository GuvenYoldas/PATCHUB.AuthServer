using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PATCHUB.AuthServer.Domain.Entities.Base;
using PATCHUB.AuthServer.Domain.Enumeration;
using PATCHUB.AuthServer.Persistence.Context;

namespace PATCHUB.AuthServer.Persistence.Repositories.Base
{
    public abstract class GenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DbContext _context;
        public GenericRepository(DbContext context)
        {
            _context = context;
        }

        public virtual void Create(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public TEntity Created(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            return entity;
        }
        public virtual void Update(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
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

                query.AsNoTracking();

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
          int? take = null,
          bool? isActive = true)

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
          int? skip = null,
          int? take = null)

        {
            return GetQueryable(
                filter: filter,
                orderBy: orderBy,
                includeProperties: includeProperties,
                skip: skip,
                take: take)
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
          string includeProperties = null,
          bool? isActive = true)

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
          string includeProperties = null,
          bool? isActive = true)

        {
            return await GetQueryable(
                filter: filter,
                orderBy: orderBy,
                includeProperties: includeProperties)
              .FirstOrDefaultAsync();
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

    public abstract class GenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        //protected readonly DbContext _context;
        protected readonly DbContext _context;

        public GenericRepository(DbContext context)
        {
            _context = context;
        }

        public virtual void Create(TEntity entity)
        {
            entity.CreateDate = DateTime.UtcNow;
            _context.Set<TEntity>().Add(entity);
        }

        public TEntity Created(TEntity entity)
        {
            entity.CreateDate = DateTime.UtcNow;
            _context.Set<TEntity>().Add(entity);
            return entity;
        }
        public virtual void Update(TEntity entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
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

        public void SoftDelete(TKey id)
        {
            TEntity entity = _context.Set<TEntity>().Find(id);
            SoftDelete(entity);
        }

        public void SoftDelete(TEntity entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            entity.StatusCode = (int)StatusCode.PASSIVE;
            Update(entity);
        }

        public virtual void SoftDeleteAll(IEnumerable<TEntity> entities)
        {
            var dbSet = _context.Set<TEntity>();

            foreach (var entity in entities)
            {
                entity.UpdateDate = DateTime.UtcNow;
                entity.StatusCode = (int)StatusCode.PASSIVE;
                Update(entity);
            }

        }

        protected virtual IQueryable<TEntity> GetQueryable(
          Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          string includeProperties = null,
          int? skip = null,
          int? take = null,
          bool? isActive = true)

        {
            try
            {
                // _context.ChangeTracker.AutoDetectChangesEnabled = false;

                includeProperties = includeProperties ?? string.Empty;
                IQueryable<TEntity> query = _context.Set<TEntity>();

                query.AsNoTracking();

                if (isActive == true)
                {
                    query = query.Where(q => q.StatusCode == (int)StatusCode.ACTIVE);
                }

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
          int? take = null,
          bool? isActive = true)

        {
            return GetQueryable(
                filter: null,
                orderBy: orderBy,
                includeProperties: includeProperties,
                skip: skip,
                take: take,
                isActive: isActive)
              .ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          string includeProperties = null,
          int? skip = null,
          int? take = null,
          bool? isActive = true)

        {
            return await GetQueryable(
                filter: null,
                orderBy: orderBy,
                includeProperties: includeProperties,
                skip: skip,
                take: take,
                isActive: isActive)
              .ToListAsync();
        }

        public virtual IEnumerable<TEntity> Get(
          Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          string includeProperties = null,
          int? skip = null,
          int? take = null,
          bool? isActive = true)

        {
            return GetQueryable(
                filter: filter,
                orderBy: orderBy,
                includeProperties: includeProperties,
                skip: skip,
                take: take,
                isActive: isActive)
              .ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(
          Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          string includeProperties = null,
          int? skip = null,
          int? take = null,
          bool? isActive = true)

        {
            return await GetQueryable(
                filter: filter,
                orderBy: orderBy,
                includeProperties: includeProperties,
                skip: skip,
                take: take,
                isActive: isActive)
              .ToListAsync();
        }

        public virtual TEntity GetOne(
          Expression<Func<TEntity, bool>> filter = null,
          string includeProperties = "",
          bool? isActive = true)

        {
            return GetQueryable(
                filter: filter,
                orderBy: null,
                includeProperties: includeProperties,
                isActive: isActive)
              .SingleOrDefault();
        }

        public virtual async Task<TEntity> GetOneAsync(
          Expression<Func<TEntity, bool>> filter = null,
          string includeProperties = null,
          bool? isActive = true)

        {
            return await GetQueryable(
                filter: filter,
                orderBy: null,
                includeProperties: includeProperties,
                isActive: isActive)
              .SingleOrDefaultAsync();
        }

        public virtual TEntity GetFirst(
          Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          string includeProperties = null,
          bool? isActive = true)

        {
            return GetQueryable(
                filter: filter,
                orderBy: orderBy,
                includeProperties: includeProperties,
                isActive: isActive)
              .FirstOrDefault();
        }

        public virtual async Task<TEntity> GetFirstAsync(
          Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          string includeProperties = null,
          bool? isActive = true)

        {
            return await GetQueryable(
                filter: filter,
                orderBy: orderBy,
                includeProperties: includeProperties,
                isActive: isActive)
              .FirstOrDefaultAsync();
        }

        public virtual TEntity GetById(
          TKey id,
          bool? isActive = true)

        {
            var query = _context.Set<TEntity>().Find(id);

            if (isActive == true && query.StatusCode != (int)StatusCode.ACTIVE)
            {
                return null;
            }

            return query;
        }

        public virtual async Task<TEntity> GetByIdAsync(
          TKey id,
          bool? isActive = true)

        {
            var query = await _context.Set<TEntity>().FindAsync(id);

            if (isActive == true && query.StatusCode != (int)StatusCode.ACTIVE)
            {
                return null;
            }

            return query;
        }

        public virtual int GetCount(
          Expression<Func<TEntity, bool>> filter = null,
          bool? isActive = true)
        {
            return GetQueryable(
                filter: filter,
                isActive: isActive)
              .Count();
        }

        public virtual async Task<int> GetCountAsync(
          Expression<Func<TEntity, bool>> filter = null,
          bool? isActive = true)

        {
            return await GetQueryable(
                filter: filter,
                isActive: isActive)
              .CountAsync();
        }

        public virtual bool GetExists(
          Expression<Func<TEntity, bool>> filter = null,
          bool? isActive = true)

        {
            return GetQueryable(
                filter: filter,
                isActive: isActive)
              .Any();
        }

        public virtual async Task<bool> GetExistsAsync(
          Expression<Func<TEntity, bool>> filter = null,
          bool? isActive = true)

        {
            return await GetQueryable(
                filter: filter,
                isActive: isActive)
              .AnyAsync();
        }

    }

}