namespace N5.Infrastructure.DataAccess
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using N5.Business.Interfaces.DataAccess;
    using System.Linq;
    using System.Linq.Expressions;

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Properties
        protected DbContext DbContext { get; set; }
        protected DbSet<TEntity> Entity { get; set; }
        #endregion

        public Repository(DbContext context)
        {
            DbContext = context;
            DbContext.ChangeTracker.LazyLoadingEnabled = true;
            DbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            Entity = DbContext.Set<TEntity>();
        }

        #region Queries
        public async Task<IEnumerable<TEntity>?> GetAllAsync()
        {
            var result = await Entity.AsNoTracking().ToListAsync();
            return result.Any() ? result : null;
        }

        public async Task<IEnumerable<TEntity>?> GetAllAsync(Expression<Func<TEntity, bool>>? where = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = Entity.AsQueryable();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            if (where != null) query = query.Where(where);

            var result = await query.AsNoTracking().ToListAsync();
            return result.Any() ? result : null;
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where)
        {
            IQueryable<TEntity> query = Entity.AsQueryable();

            return await query.AsNoTracking().AnyAsync(where);
        }

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = Entity.AsQueryable();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(where);
        }

        #endregion

        #region Commands
        public async Task<bool> InsertAsync(TEntity entity)
        {
            await DbContext.AddAsync(entity);

            return await DbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;

            return await DbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            DbContext.Remove(entity);

            return await DbContext.SaveChangesAsync() > 0;
        }
        #endregion
    }
}