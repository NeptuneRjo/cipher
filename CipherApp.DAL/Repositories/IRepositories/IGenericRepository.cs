using System.Linq.Expressions;

namespace CipherApp.DAL.Repositories.IRepositories
{
    public interface IGenericRepository<TEntity>
    {
        /// <summary>
        /// Add the entity asynchronosously
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>The added <see cref="TEntity"/></returns>
        Task<TEntity> AddEntityAsync(TEntity entity);

        /// <summary>
        /// Add a range of entities asynchronously
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>The added <see cref="ICollection{TEntity}"/></returns>
        Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities);

        /// <summary>
        /// Gets all of the entities asynchronously
        /// </summary>
        /// <returns><see cref="ICollection{TEntity}"/></returns>
        Task<ICollection<TEntity>> GetAllAsync();

        /// <summary>
        /// Filter the database for a specific entity asynchronously. Add expression for child elements
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includes"></param>
        /// <returns>The found <see cref="TEntity"/></returns>
        Task<TEntity> GetByQueryAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[]? includes);

        /// <summary>
        /// Filter the database for a collection of entities asynchronously. Add expression for child elements
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includes"></param>
        /// <returns>The found <see cref="ICollection{TEntity}"/></returns>
        Task<ICollection<TEntity>> GetAllByQueryAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[]? includes);

        /// <summary>
        /// Update an entity asynchronously
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>The updated <see cref="TEntity"/></returns>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// Check if entity exists asynchronously
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>true if the entity exists</returns>
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Delete the entity asynchronously
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// Save changes to the database asynchronously
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();
    }
}
