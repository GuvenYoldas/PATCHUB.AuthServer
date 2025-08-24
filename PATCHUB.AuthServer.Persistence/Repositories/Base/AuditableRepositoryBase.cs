using Microsoft.EntityFrameworkCore;
using PATCHUB.AuthServer.Domain.Common.Primitives;
using PATCHUB.AuthServer.Domain.Enumeration;
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
    public abstract class AuditableRepositoryBase<TEntity, TKey> : IAuditableRepositoryBase<TEntity, TKey>
    where TEntity : AuditableEntity<TKey>
    {
        protected readonly DbContext _context;
        protected readonly IClientCredentialAccessor _clientCredentialAccessor;

        public AuditableRepositoryBase(DbContext context, IClientCredentialAccessor clientCredentialAccessor)
        {
            _context = context;
            _clientCredentialAccessor = clientCredentialAccessor;
        }

        public TEntity Add(TEntity entity)
        {
            SetAuditFields(entity);
            _context.Set<TEntity>().Add(entity);
            return entity;
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            SetAuditFields(entity);
            await _context.Set<TEntity>().AddAsync(entity);
            return entity;
        }
        public virtual void Update(TEntity entity)
        {
            UpdateAuditFields(entity);
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
            UpdateAuditFields(entity);
            entity.Status = EnumStatusCode.PASSIVE;
            Update(entity);
        }

        public virtual void SoftDeleteAll(IEnumerable<TEntity> entities)
        {
            var dbSet = _context.Set<TEntity>();

            foreach (var entity in entities)
            {
                UpdateAuditFields(entity);
                entity.Status = EnumStatusCode.PASSIVE;
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

                query = query.AsNoTracking();

                if (isActive == true)
                {
                    query = query.Where(q => q.Status == EnumStatusCode.ACTIVE);
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
            TEntity query = null;

            if (isActive == true)
            {
                query = _context.Set<TEntity>().FirstOrDefault(x => x.ID.Equals(id) && x.Status == EnumStatusCode.ACTIVE);
            }
            else
            {
                query = _context.Set<TEntity>().Find(id);
            }

            return query;
        }

        public virtual async Task<TEntity> GetByIdAsync(
          TKey id,
          bool? isActive = true)

        {
            TEntity query = null;


            if (isActive == true)
            {
                query = await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.ID.Equals(id) && x.Status == EnumStatusCode.ACTIVE);
            }
            else
            {
                query = await _context.Set<TEntity>().FindAsync(id);
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

        private void SetAuditFields(TEntity entity)
        {
            entity.CreateDate = DateTime.UtcNow;
            entity.CreateIp = _clientCredentialAccessor?.ClientIp;
            entity.CreateUserId = _clientCredentialAccessor?.UserId;
        }

        private void UpdateAuditFields(TEntity entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            entity.UpdateIp = _clientCredentialAccessor?.ClientIp;
            entity.UpdateUserId = _clientCredentialAccessor?.UserId;

        }

    }
}
