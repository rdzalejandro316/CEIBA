using Microsoft.EntityFrameworkCore;
using PruebaIngresoBibliotecario.Infrastructure.DataAccess;
using PruebaIngresoBibliotecario.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Infrastructure.Services
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public Repository(PersistenceContext dbContext)
        {
            DbContext = dbContext;
            DbContext.ChangeTracker.LazyLoadingEnabled = false;
            DbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            Entity = DbContext.Set<TEntity>();
        }

        #region Properties
        protected DbContext DbContext { get; set; }
        protected DbSet<TEntity> Entity { get; set; }
        #endregion

        #region Queries
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Entity.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = Entity.AsQueryable();
            query.PerformInclusions(includeProperties);

            return await query.Where(where).ToArrayAsync();
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = Entity.AsQueryable();
            query.PerformInclusions(includeProperties);

            return await query.FirstOrDefaultAsync(where);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where)
        {
            IQueryable<TEntity> query = Entity.AsQueryable();

            return await query.AnyAsync(where);
        }
        #endregion

        #region Commands
        public async Task<bool> InsertAsync(TEntity entity)
        {
            await DbContext.AddAsync(entity);

            return await DbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> InsertAsync(IEnumerable<TEntity> entities)
        {
            await DbContext.AddRangeAsync(entities);

            return await DbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;

            return await DbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(IEnumerable<TEntity> entities)
        {
            DbContext.Entry(entities).State = EntityState.Modified;

            return await DbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            DbContext.Remove(entity);

            return await DbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(IEnumerable<TEntity> entities)
        {
            DbContext.Remove(entities);

            return await DbContext.SaveChangesAsync() > 0;
        }
        #endregion
    }

    public static class QueryExtension
    {
        /// <summary>
        /// Include related entities.
        /// </summary>
        /// <param name="source">The Source.</param>
        /// <param name="includeProperties">The Properties.</param>
        /// <returns>IQueryable&lt;TEntity&gt;.</returns>
        public static IQueryable<TEntity> PerformInclusions<TEntity>(this IQueryable<TEntity> source, IEnumerable<Expression<Func<TEntity, object>>> includeProperties) where TEntity : class
        {
            return includeProperties.Aggregate(source, (current, includeProperties) => current.Include(includeProperties));
        }
    }
}
