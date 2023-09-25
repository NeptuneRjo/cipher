using CipherApp.DAL.Data;
using CipherApp.DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CipherApp.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DataContext _context;

        public GenericRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return entities;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter) =>
            await _context.Set<TEntity>().AsNoTracking().AnyAsync(filter);

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();

        public async Task<ICollection<TEntity>> GetAllAsync() =>
            await _context.Set<TEntity>().ToListAsync();

        public async Task<ICollection<TEntity>> GetAllByQueryAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>>[]? includes = null) =>
            await Queryable(includes).Where(filter).ToListAsync();

        public async Task<TEntity> GetByQueryAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>>[]? includes = null) =>
            await Queryable(includes).FirstOrDefaultAsync(filter);

        private IQueryable<TEntity> Queryable(Expression<Func<TEntity, object>>[]? includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }
    }
}
