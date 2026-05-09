using System.Linq.Expressions;
using CQRS.Domain.Consts;
using CQRS.Domain.Entities;
using CQRS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CQRS.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Base
    {
        protected readonly ApplicationDBContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(ApplicationDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            foreach(var include in includes)
            {
                query = query.Include(include);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach(var include in includes)
            {
                query = query.Include(include);
            }
            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(criteria);
        }

        public async Task<IEnumerable<T>> WhereAsync(
            Expression<Func<T, bool>> criteria,
            int? skip = null,
            int? take = null,
            Expression<Func<T, object>>? orderBy = null,
            OrderBy orderDirection = OrderBy.Ascending,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            query = query.Where(criteria);

            if (orderBy != null)
            {
                query = orderDirection == OrderBy.Ascending
                    ? query.OrderBy(orderBy)
                    : query.OrderByDescending(orderBy);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return await query.ToListAsync();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
        public void SaveInclude(T entity, params string[] includedProperties)
        {
            var LocalEntity = _dbSet.Local.FirstOrDefault(e => e.Id == entity.Id);
            EntityEntry entry;

            if (LocalEntity == null)
            {
                //_dbSet.Attach(entity);
                entry = _context.Entry(entity);
            }
            else
            {
                entry = _context.ChangeTracker.Entries<T>().First(e => e.Entity.Id == entity.Id);
            }

            foreach (var property in entry.Properties)
            {
                if (property.Metadata.IsPrimaryKey())
                    continue;
                else
                {
                    if (includedProperties.Contains(property.Metadata.Name))
                    {
                        property.IsModified = true;
                    }
                    else
                    {
                        property.IsModified = false;
                    }
                }

            }

        }

        
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? criteria = null)
        {
            if (criteria == null)
            {
                return await _dbSet.CountAsync();
            }

            return await _dbSet.CountAsync(criteria);
        }

        // Keep your existing additional methods as they are useful
        public IQueryable<T> Query()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }



        // Extension methods to support LINQ operations
        public async Task<List<T>> ToListAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TResult> MaxAsync<TResult>(Expression<Func<T, TResult>> selector)
        {
            return await _dbSet.MaxAsync(selector);
        }

        public async Task<TResult> MinAsync<TResult>(Expression<Func<T, TResult>> selector)
        {
            return await _dbSet.MinAsync(selector);
        }

        // Fixed SumAsync and AverageAsync methods
        public async Task<int> SumAsync(Expression<Func<T, int>> selector)
        {
            return await _dbSet.SumAsync(selector);
        }

        public async Task<long> SumAsync(Expression<Func<T, long>> selector)
        {
            return await _dbSet.SumAsync(selector);
        }

        public async Task<decimal> SumAsync(Expression<Func<T, decimal>> selector)
        {
            return await _dbSet.SumAsync(selector);
        }

        public async Task<double> SumAsync(Expression<Func<T, double>> selector)
        {
            return await _dbSet.SumAsync(selector);
        }

        public async Task<float> SumAsync(Expression<Func<T, float>> selector)
        {
            return await _dbSet.SumAsync(selector);
        }

        public async Task<int?> SumAsync(Expression<Func<T, int?>> selector)
        {
            return await _dbSet.SumAsync(selector);
        }

        public async Task<long?> SumAsync(Expression<Func<T, long?>> selector)
        {
            return await _dbSet.SumAsync(selector);
        }

        public async Task<decimal?> SumAsync(Expression<Func<T, decimal?>> selector)
        {
            return await _dbSet.SumAsync(selector);
        }

        public async Task<double?> SumAsync(Expression<Func<T, double?>> selector)
        {
            return await _dbSet.SumAsync(selector);
        }

        public async Task<float?> SumAsync(Expression<Func<T, float?>> selector)
        {
            return await _dbSet.SumAsync(selector);
        }

        // AverageAsync methods
        public async Task<double> AverageAsync(Expression<Func<T, int>> selector)
        {
            return await _dbSet.AverageAsync(selector);
        }

        public async Task<double> AverageAsync(Expression<Func<T, long>> selector)
        {
            return await _dbSet.AverageAsync(selector);
        }

        public async Task<decimal> AverageAsync(Expression<Func<T, decimal>> selector)
        {
            return await _dbSet.AverageAsync(selector);
        }

        public async Task<double> AverageAsync(Expression<Func<T, double>> selector)
        {
            return await _dbSet.AverageAsync(selector);
        }

        public async Task<float> AverageAsync(Expression<Func<T, float>> selector)
        {
            return await _dbSet.AverageAsync(selector);
        }

        public async Task<double?> AverageAsync(Expression<Func<T, int?>> selector)
        {
            return await _dbSet.AverageAsync(selector);
        }

        public async Task<double?> AverageAsync(Expression<Func<T, long?>> selector)
        {
            return await _dbSet.AverageAsync(selector);
        }

        public async Task<decimal?> AverageAsync(Expression<Func<T, decimal?>> selector)
        {
            return await _dbSet.AverageAsync(selector);
        }

        public async Task<double?> AverageAsync(Expression<Func<T, double?>> selector)
        {
            return await _dbSet.AverageAsync(selector);
        }

        public async Task<float?> AverageAsync(Expression<Func<T, float?>> selector)
        {
            return await _dbSet.AverageAsync(selector);
        }

        Task<IEnumerable<T>> IBaseRepository<T>.WhereAsync(Expression<Func<T, bool>> criteria, int? skip, int? take, Expression<Func<T, object>>? orderBy, OrderBy orderDirection, params Expression<Func<T, object>>[] includes)
        {
            throw new NotImplementedException();
        }
    }
}
