using Gyneco.Application.Models.Search;
using Gyneco.Domain.Common;
using System.Linq.Expressions;

namespace Gyneco.Application.Contracts.Peristence
{
    public interface IGenericRepository<Entity> where Entity : class
    {
        // Query Methods
        IQueryable<Entity> GetQuery();
        IQueryable<Entity> GetQuery(string linkedElements);
        IQueryable<Entity> FilterQuery(IQueryable<Entity> query, Expression<Func<Entity, bool>> whereExpression);
        IOrderedQueryable<Entity> OrderQuery<T>(IQueryable<Entity> query, Expression<Func<Entity, T>> keySelector, bool IsAscending = true);
        IOrderedQueryable<Entity> OrderQuery<T>(IOrderedQueryable<Entity> query, Expression<Func<Entity, T>> keySelector, bool IsAscending = true);
        IQueryable<Entity> FilterQuery(IQueryable<Entity> query, string navigationPath, Expression<Func<Entity, bool>> whereExpression);

        // CRUD Operations
        Task<Entity> CreateAsync(Entity entity);
        Task<Entity> UpdateAsync(Entity entity);
        Task<Entity> DeleteAsync(Guid entityId);
        Task<Entity> DeleteAsync(Entity entity);
        Task<bool> DeleteAsync(Expression<Func<Entity, bool>> whereExpression);

        // Retrieval Methods
        Task<IEnumerable<Entity>> AllAsync();
        Task<IEnumerable<Class>> AllIncludeAsync<Class>(string navigationPath) where Class : class;
        Task<SearchResult<Entity>> AllPageAsync(int pageIndex, int pageSize);
        Task<SearchResult<Entity>> AllPageAsync(IQueryable<Entity> query, int pageIndex, int pageSize);

        // Count Methods
        Task<int> CountAsync(Expression<Func<Entity, bool>> whereExpression);
        Task<int> CountIncludeAsync<Class>(string navigationPath, Expression<Func<Entity, bool>> whereExpression) where Class : class;

        // Existence Check Methods
        Task<bool> ExistsAsync(Expression<Func<Entity, bool>> whereExpression);
        Task<bool> ExistsIncludeAsync(string navigationPath, Expression<Func<Entity, bool>> whereExpression);

        // Find Methods
        Task<Entity> FindAsync(Guid id);
        Task<Entity> FindAsync(Expression<Func<Entity, bool>> whereExpression);
        Task<IEnumerable<Entity>> FindAllAsync(Expression<Func<Entity, bool>> whereExpression);
        Task<SearchResult<Entity>> FindAllPageAsync(int pageIndex, int pageSize, Expression<Func<Entity, bool>> whereExpression);

        // Select Methods
        Task<Class> FindSelectAsync<Class>(Expression<Func<Entity, bool>> whereExpression, Expression<Func<Entity, object>> selectExpression) where Class : class;
        Task<IEnumerable<Class>> FindAllSelectAsync<Class>(Expression<Func<Entity, bool>> whereExpression, Expression<Func<Entity, object>> selectExpression) where Class : class;

        // Include Methods
        Task<IEnumerable<Class>> FindAllIncludeAsync<Class>(string navigationPath, Expression<Func<Entity, bool>> whereExpression, Expression<Func<Entity, object>> selectExpression) where Class : class;
        Task<IEnumerable<Class>> FindAllIncludeAsync<Class>(string navigationPath, Expression<Func<Entity, bool>> whereExpression) where Class : class;
        Task<Class> FindIncludeAsync<Class>(string navigationPath, Expression<Func<Entity, bool>> whereExpression, Expression<Func<Entity, object>> selectExpression) where Class : class;

        // SQL Execution Methods
        Task ExecuteSqlCommandAsync(string sql, params object[] parameters);
        Task ExecuteSqlAsync(FormattableString sql);
    }
}
