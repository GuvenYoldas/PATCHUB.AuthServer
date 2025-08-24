using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PATCHUB.AuthServer.Domain.Repositories.Base
{
  public interface IRepositoryBase<TEntity, TKey> where TEntity : class
    {
        public TEntity Add(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TKey id);
        void Delete(TEntity entity);
        void DeleteAll(IEnumerable<TEntity> entities);
        IEnumerable<TEntity> GetAll(
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          string includeProperties = null,
          int? skip = null,
          int? take = null);
        Task<IEnumerable<TEntity>> GetAllAsync(
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          string includeProperties = null,
          int? skip = null,
          int? take = null);
        IEnumerable<TEntity> Get(
          Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          string includeProperties = null,
          int? skip = null);
        Task<IEnumerable<TEntity>> GetAsync(
          Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          string includeProperties = null,
          int? skip = null,
          int? take = null);
        TEntity GetOne(
          Expression<Func<TEntity, bool>> filter = null,
          string includeProperties = "");
        Task<TEntity> GetOneAsync(
          Expression<Func<TEntity, bool>> filter = null,
          string includeProperties = null);
        TEntity GetFirst(
          Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          string includeProperties = null);
        Task<TEntity> GetFirstAsync(
          Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          string includeProperties = null);
        TEntity GetById(TKey id);
        Task<TEntity> GetByIdAsync(TKey id);
        int GetCount(
          Expression<Func<TEntity, bool>> filter = null);
        Task<int> GetCountAsync(
          Expression<Func<TEntity, bool>> filter = null);
        bool GetExists(
          Expression<Func<TEntity, bool>> filter = null);
        Task<bool> GetExistsAsync(
          Expression<Func<TEntity, bool>> filter = null);

    }
}
