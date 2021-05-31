using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistance;
using Ordering.Domain.Common;
using Ordering.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class RepositotyBase<T> : IAsyncRepository<T> where T : BaseEntity
    {
        protected readonly OrderContext _dbContext;
        private readonly ILogger<RepositotyBase<T>> _logger;
        private readonly DbSet<T> _db;
        public RepositotyBase(OrderContext dbContext, ILogger<RepositotyBase<T>> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
            _db = dbContext.Set<T>();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _db.ToListAsync();
        }
        public async Task<T> CreateAsync(T model)
        {
            await _db.AddAsync(model);
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
                return model;
            return default;
        }

        public async Task DeleteAsync(int id)
        {
            var item = await GetAsyncById(id);
            if (item != null)
            {
                _db.Remove(item);
                await _dbContext.SaveChangesAsync();
            }
        }


        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predict)
        {
            return await _db.Where(predict).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predict = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, string includeString = null, bool disableTracking = true)
        {
            var query = _db.AsQueryable();
            if (disableTracking)
                query = query.AsNoTracking();
            if (!string.IsNullOrEmpty(includeString))
                query = query.Include(includeString);
            if (predict != null)
                query = query.Where(predict);
            if (orderby != null)
                return await orderby(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predict = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
        {
            var query = _db.AsQueryable();
            if (disableTracking)
                query = query.AsNoTracking();
            if (includes != null)
                query = includes.Aggregate(query, (current, include) => current.Include(include.ToString()));
            if (predict != null)
                query = query.Where(predict);
            if (orderby != null)
                return await orderby(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<T> GetAsyncById(int id)
        {
            return await _db.FindAsync(id);
        }

        public async Task UpdateAsync(T model)
        {
            _dbContext.Entry(model).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
