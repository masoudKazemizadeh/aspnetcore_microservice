using Ordering.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Contracts.Persistance
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predict);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predict = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
            string includeString = null,
            bool disableTracking = true
            );
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predict = null,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
           List<Expression<Func<T, object>>> includes = null,
           bool disableTracking = true
           );

        Task<T> GetAsyncById(int id);
        Task<T> CreateAsync(T model);
        Task UpdateAsync(T model);
        Task DeleteAsync(int id);
    }
}
