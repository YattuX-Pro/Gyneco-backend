
using Gyneco.Domain.Common;
using Gyneco.Persistence.DatabaseContext;
using Kada.Application.Contracts.Pesistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Kada.persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly GynecoDbContext _context;

        public GenericRepository(GynecoDbContext context)
        {
            this._context = context;
        }

        public async Task CreateAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> whereExpression)
        {
            return await _context.Set<T>().AnyAsync(whereExpression);
        }

        public virtual IQueryable<T> GetQuery()
        {
            return _context.Set<T>().AsQueryable();
        }

        public IQueryable<T> GetQuery(string linkedElements)
        {
            string[] splited = linkedElements.Split(',');

            IQueryable<T> query = _context.Set<T>().AsQueryable();

            foreach (string element in splited)
            {
                query = query.Include(element);
            }

            return query;
        }

        public IQueryable<T> FilterQuery(IQueryable<T> query, Expression<Func<T, bool>> whereExpression)
        {
            return query.Where(whereExpression);
        }
    }
}
