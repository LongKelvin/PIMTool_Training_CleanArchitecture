using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using ProjectManagement.Application.Interfaces;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Infrastructure.Data.DataContext;

namespace ProjectManagement.Infrastructure.Repositories
{
    public class Repository<T>(PIMToolDbContext context) : IRepository<T> where T : BaseEntity
    {
        private readonly PIMToolDbContext _context = context;
        private readonly DbSet<T> _dbSet = context.Set<T>();

        public async Task<T?> GetByIdAsync(Guid id, Func<IQueryable<T>, IQueryable<T>>? include = null, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbSet;

            if (include != null)
            {
                query = include(query);
            }

            return await query.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
        }


        public async Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>>? include = null, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbSet;

            if (include != null)
            {
                query = include(query);
            }

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbSet.Where(predicate);
            return await query.ToListAsync(cancellationToken);
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _dbSet.FindAsync([id], cancellationToken);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        private static IQueryable<T> ApplyIncludes(IQueryable<T> query, string? relatedEntity = null)
        {
            if (!string.IsNullOrEmpty(relatedEntity))
                return query.Include($"{relatedEntity}");

            return query;
        }
    }
}