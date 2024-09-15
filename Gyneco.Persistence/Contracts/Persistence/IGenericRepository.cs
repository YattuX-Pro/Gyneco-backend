using Gyneco.Domain.Common;
using System.Linq.Expressions;

namespace Kada.Application.Contracts.Pesistence
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> whereExpression);
        IQueryable<T> GetQuery();
        IQueryable<T> GetQuery(string linkedElements);
        IQueryable<T> FilterQuery(IQueryable<T> query, Expression<Func<T, bool>> whereExpression);
    }
}
