using System.Linq.Expressions;

namespace PATCHUB.AuthServer.Domain.Repositories.Base
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(TKey id, bool? isActive = true);
        TEntity GetById(TKey id, bool? isActive = true);

        Task<TEntity> GetOneAsync(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null,
            bool? isActive = true);

        TEntity GetOne(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = "",
            bool? isActive = true);

        Task<TEntity> GetFirstAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            bool? isActive = true);

        TEntity GetFirst(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            bool? isActive = true);

        Task<IEnumerable<TEntity>> GetAllAsync(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null,
            bool? isActive = true);

        IEnumerable<TEntity> GetAll(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null,
            bool? isActive = true);

        Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null,
            bool? isActive = true);

        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null,
            bool? isActive = true);

        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null, bool? isActive = true);
        int GetCount(Expression<Func<TEntity, bool>> filter = null, bool? isActive = true);

        Task<bool> GetExistsAsync(Expression<Func<TEntity, bool>> filter = null, bool? isActive = true);
        bool GetExists(Expression<Func<TEntity, bool>> filter = null, bool? isActive = true);

        void Create(TEntity entity);
        TEntity Created(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(TKey id);
        void DeleteAll(IEnumerable<TEntity> entities);

        void SoftDelete(TKey id);
        void SoftDelete(TEntity entity);
        void SoftDeleteAll(IEnumerable<TEntity> entities);
    }
}