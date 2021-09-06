using AppCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AppCore.Data
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity, new()
    {
        /// <summary>
        /// Return object according to id
        /// </summary>
        TEntity GetById(int id);
        /// <summary>
        /// Return object asynchronously according to id
        /// </summary>
        Task<TEntity> GetByIdAsync(int id);
        /// <summary>
        /// Return object according to filter and if exist, adds Include
        /// </summary>
        TEntity Get(Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>
        /// Return object according to filter and if exist, adds Include
        /// </summary>
        IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>
        /// Return object according to filter asynchronously and if exist, adds Include
        /// </summary>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, object>>[] includeProperties);
        /// <summary>
        /// Return list of objects according to filter and if exist, adds Include
        /// </summary>
        IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] includeProperties);
        /// <summary>
        /// Return list of objects according to filter and if exist, adds Include
        /// </summary>
        IQueryable<TEntity> GetListQueryable(Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] includeProperties);
        /// <summary>
        /// Return list of objects according to filter asynchronously and if exist, adds Include
        /// </summary>
        Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] includeProperties);
        /// <summary>
        /// Add new object
        /// </summary>
        void Add(TEntity entity);
        /// <summary>
        /// Add new object asynchronously
        /// </summary>
        Task AddAsync(TEntity entity);
        /// <summary>
        /// Add new objects at a time
        /// </summary>
        void AddRange(IEnumerable<TEntity> entities);
        /// <summary>
        /// Add new objects at a time asynchronously
        /// </summary>
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        /// <summary>
        /// Updates object that exist 
        /// </summary>
        void Update(TEntity entity);
        /// <summary>
        /// Change the flag of object and softly delete or if IsHardDelete is true, remove the object completely
        /// </summary>
        void Delete(int id, bool IsHardDelete = false);
        /// <summary>
        /// Change the flag of object and softly delete or if IsHardDelete is true, remove the object completely
        /// </summary>
        void Delete(TEntity entity, bool IsHardDelete = false);
        /// <summary>
        /// Change the flag of objects and softly delete or if IsHardDelete is true, remove objects completely
        /// </summary>
        void DeleteAll(IEnumerable<TEntity> entities, bool IsHardDelete = false);
        /// <summary>
        /// Return count of the objects
        /// </summary>
        int Count(Expression<Func<TEntity, bool>> filter = null);
        /// <summary>
        /// Return count of the objects asynchronously
        /// </summary>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null);
        /// <summary>
        /// Check if the object exist
        /// </summary>
        bool Any(Expression<Func<TEntity, bool>> filter);
        /// <summary>
        /// Check if the object exist asynchronously
        /// </summary>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter);
    }
}
