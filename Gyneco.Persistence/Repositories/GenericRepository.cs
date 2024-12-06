using Gyneco.Application.Models.Search;
using Gyneco.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;
using Gyneco.Domain.Contracts.Persistence;

namespace Kada.persistence.Repositories
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class
    {
        protected readonly GynecoDbContext _context;
        internal DbSet<Entity> DbSet;

        public GenericRepository(GynecoDbContext context)
        {
            this._context = context;
            DbSet = _context.Set<Entity>();
        }

        #region FilterQuery
        public virtual IQueryable<Entity> GetQuery()
        {
            return DbSet.AsQueryable();
        }

        public IQueryable<Entity> GetQuery(string linkedElements)
        {
            string[] splited = linkedElements.Split(',');

            IQueryable<Entity> query = DbSet.AsQueryable();

            foreach (string element in splited)
            {
                query = query.Include(element);
            }

            return query;
        }

        public IQueryable<Entity> FilterQuery(IQueryable<Entity> query, Expression<Func<Entity, bool>> whereExpression)
        {
            return query.Where(whereExpression);
        }

        public IOrderedQueryable<Entity> OrderQuery<T>(IQueryable<Entity> query, Expression<Func<Entity, T>> keySelector, bool IsAscending = true)
        {
            return IsAscending ? query.OrderBy(keySelector) : query.OrderByDescending(keySelector);
        }

        public IOrderedQueryable<Entity> OrderQuery<T>(IOrderedQueryable<Entity> query, Expression<Func<Entity, T>> keySelector, bool IsAscending = true)
        {
            return IsAscending ? query.ThenBy(keySelector) : query.ThenByDescending(keySelector);
        }

        public IQueryable<Entity> FilterQuery(IQueryable<Entity> query, string navigationPath, Expression<Func<Entity, bool>> whereExpression)
        {
            return query.Include(navigationPath).Where(whereExpression);
        }
        #endregion

        #region GenericMethods
        //Create
        /// <inheritdoc/>
        public async Task<Entity> CreateAsync(Entity entity)
        {
            int result = 0;

            try
            {
                var creationDateProperty = typeof(Entity).GetProperty("CreationTime");
                if (creationDateProperty != null && creationDateProperty.GetValue(entity) != null) creationDateProperty.SetValue(entity, DateTime.UtcNow);

                var IdProperty = typeof(Entity).GetProperty("Id");
                if (IdProperty != null && IdProperty.GetValue(entity) != null && IdProperty.PropertyType.Name != "Int32") IdProperty.SetValue(entity, Guid.Empty);

                var StatusProperty = typeof(Entity).GetProperty("Status");
                if (StatusProperty != null && (int)StatusProperty.GetValue(entity) == 0) StatusProperty.SetValue(entity, 1);

                await DbSet.AddAsync(entity);
                result = await _context.SaveChangesAsync();
            }
            catch (System.Exception e)
            {
                throw e;
            }
            return entity;
        }

        //Update
        /// <inheritdoc/>
        public async Task<Entity> UpdateAsync(Entity entity)
        {
            int result = 0;

            try
            {
                PropertyInfo entityIdProperty = typeof(Entity).GetProperty("Id");
                if (entityIdProperty.PropertyType.Name == "Int32")
                {
                    var id = (int)entityIdProperty.GetValue(entity);
                    _context.Entry(await DbSet.FindAsync(id)).State = EntityState.Detached;
                }
                else
                {
                    var id = (Guid)entityIdProperty.GetValue(entity);
                    _context.Entry(await DbSet.FindAsync(id)).State = EntityState.Detached;
                }
                _context.Entry(entity).State = EntityState.Modified;
                result = await _context.SaveChangesAsync();
            }
            catch (System.Exception e)
            {
                throw e;
            }
            return entity;
        }

        //Overrides Default Delete
        /// <inheritdoc/>
        public async Task<Entity> DeleteAsync(Guid entityId)
        {
            int result = 0;

            try
            {
                var entity = await FindAsync(entityId);

                if (entity == null)
                {
                    throw new Exception($"Can't find {typeof(Entity)} in the DB");
                }

                DbSet.Remove(entity);
                result = await _context.SaveChangesAsync();

                return entity;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <inheritdoc/>
        public async Task<Entity> DeleteAsync(Entity entity)
        {
            int result = 0;

            try
            {
                var toDelete = await DbSet.FindAsync(entity);
                if (toDelete == null)
                {
                    throw new Exception($"Can't find {typeof(Entity)} in the DB");
                }

                DbSet.Remove(entity);
                result = await _context.SaveChangesAsync();

                return entity;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(Expression<Func<Entity, bool>> whereExpression)
        {
            try
            {
                var toDelete = await DbSet.Where(whereExpression).ToListAsync();

                foreach (Entity e in toDelete)
                {
                    DbSet.Remove(e);
                    await _context.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //All
        /// <inheritdoc/>
        public async Task<IEnumerable<Entity>> AllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<IEnumerable<Class>> AllIncludeAsync<Class>(string navigationPath) where Class : class
        {
            List<Class> entityList = null;

            try
            {
                entityList = await DbSet.Include(navigationPath).Cast<Class>().ToListAsync();
            }
            catch (System.Exception e)
            {

                throw e;
            }

            return entityList;
        }

        //AllPage
        /// <inheritdoc/>
        public async Task<SearchResult<Entity>> AllPageAsync(int pageIndex, int pageSize)
        {
            SearchResult<Entity> result = new SearchResult<Entity>();

            result.Page = pageIndex;
            result.CountPerPage = pageSize;
            result.TotalCount = await DbSet.CountAsync();
            result.Results = await DbSet.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();

            return result;
        }

        //Count
        /// <inheritdoc/>
        public async Task<int> CountAsync(Expression<Func<Entity, bool>> whereExpression)
        {
            return await DbSet.Where(whereExpression).CountAsync();
        }

        public async Task<int> CountIncludeAsync<Class>(string navigationPath, Expression<Func<Entity, bool>> whereExpression) where Class : class
        {
            int count;
            try
            {
                count = await DbSet.Include(navigationPath).Where(whereExpression).CountAsync();
            }
            catch (System.Exception e)
            {
                throw e;
            }
            return count;
        }

        //Exists
        /// <inheritdoc/>
        public async Task<bool> ExistsAsync(Expression<Func<Entity, bool>> whereExpression)
        {
            return await DbSet.AnyAsync(whereExpression);
        }

        public async Task<bool> ExistsIncludeAsync(string navigationPath, Expression<Func<Entity, bool>> whereExpression)
        {
            return await DbSet.Include(navigationPath).AnyAsync(whereExpression);
        }

        //Find
        /// <inheritdoc/>
        public async Task<Entity> FindAsync(Guid id)
        {
            Entity entity = null;

            try
            {
                entity = await DbSet.FindAsync(id);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            return entity;
        }

        /// <inheritdoc/>
        public async Task<Entity> FindAsync(Expression<Func<Entity, bool>> whereExpression)
        {
            Entity entity = null;

            try
            {
                entity = await DbSet.Where(whereExpression).SingleOrDefaultAsync();
            }
            catch (System.Exception e)
            {
                throw e;
            }
            return entity;
        }

        //FindAll
        /// <inheritdoc/>
        public async Task<IEnumerable<Entity>> FindAllAsync(Expression<Func<Entity, bool>> whereExpression)
        {
            List<Entity> entityList = null;

            try
            {
                entityList = await DbSet.Where(whereExpression).ToListAsync();
            }
            catch (System.Exception e)
            {
                throw e;
            }
            return entityList;
        }

        //FindAllPage
        /// <inheritdoc/>
        public async Task<SearchResult<Entity>> FindAllPageAsync(int pageIndex, int pageSize, Expression<Func<Entity, bool>> whereExpression)
        {
            SearchResult<Entity> result = new SearchResult<Entity>();

            try
            {
                result.Page = pageIndex;
                result.CountPerPage = pageSize;
                result.TotalCount = await DbSet.Where(whereExpression).CountAsync();
                result.Results = await DbSet.Where(whereExpression).Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
            }
            catch (System.Exception e)
            {
                throw e;
            }
            return result;
        }
        #endregion

        #region Search expressions
        //FindSelect
        /// <inheritdoc/>
        public async Task<Class> FindSelectAsync<Class>(Expression<Func<Entity, bool>> whereExpression, Expression<Func<Entity, object>> selectExpression) where Class : class
        {
            Class entity = null;

            try
            {
                entity = await DbSet.Where(whereExpression).Select(selectExpression).Cast<Class>()
                    .SingleOrDefaultAsync();

                return entity;
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }


        //FindAllSelect
        /// <inheritdoc/>
        /// 
        public async Task<IEnumerable<Class>> FindAllSelectAsync<Class>(Expression<Func<Entity, bool>> whereExpression, Expression<Func<Entity, object>> selectExpression) where Class : class
        {
            List<Class> entityList = null;

            try
            {
                entityList = await DbSet.Where(whereExpression).Select(selectExpression).Cast<Class>().ToListAsync();
                return entityList;
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region Search with Join/Subslect
        /// <inheritdoc/>
        public async Task<IEnumerable<Class>> FindAllIncludeAsync<Class>(string navigationPath, Expression<Func<Entity, bool>> whereExpression, Expression<Func<Entity, object>> selectExpression) where Class : class
        {
            List<Class> entityList = null;

            try
            {
                entityList = await DbSet.Include(navigationPath).Where(whereExpression).Select(selectExpression).Cast<Class>().ToListAsync();
            }
            catch (System.Exception e)
            {

                throw e;
            }

            return entityList;
        }

        public async Task<IEnumerable<Class>> FindAllIncludeAsync<Class>(string navigationPath, Expression<Func<Entity, bool>> whereExpression) where Class : class
        {
            List<Class> entityList = null;

            try
            {
                string[] splited = navigationPath.Split(',');

                var query = DbSet.Where(whereExpression);

                foreach (string element in splited)
                {
                    query = query.Include(element);
                }

                entityList = await query.Cast<Class>().ToListAsync();
            }
            catch (System.Exception e)
            {

                throw e;
            }

            return entityList;
        }

        /// <inheritdoc/>
        public async Task<Class> FindIncludeAsync<Class>(string navigationPath, Expression<Func<Entity, bool>> whereExpression, Expression<Func<Entity, object>> selectExpression) where Class : class
        {
            Class entity = null;

            try
            {
                string[] splited = navigationPath.Split(',');

                var query = DbSet.Where(whereExpression).Select(selectExpression);

                foreach (string element in splited)
                {
                    query = query.Include(element);
                }

                entity = await query.Cast<Class>().SingleOrDefaultAsync();
            }
            catch (System.Exception e)
            {

                throw e;
            }

            return entity;
        }
        #endregion

        #region Util
        //GetId
        /// <inheritdoc/>
        private Guid GetId(object entity)
        {
            PropertyInfo entityIdProperty = typeof(Entity).GetProperty("Id");
            return (Guid)entityIdProperty.GetValue(entity);
        }
        #endregion

        //AllPage
        /// <inheritdoc/>
        public async Task<SearchResult<Entity>> AllPageAsync(IQueryable<Entity> query, int pageIndex, int pageSize)
        {
            SearchResult<Entity> result = new SearchResult<Entity>();

            result.Page = pageIndex;
            result.CountPerPage = pageSize;
            result.TotalCount = query.Count();
            result.Results = await query.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();

            return result;
        }

        public async Task ExecuteSqlCommandAsync(string sql, params object[] parameters)
        {
            await _context.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        public async Task ExecuteSqlAsync(FormattableString sql)
        {
            await _context.Database.ExecuteSqlAsync(sql);
        }
    }
}
