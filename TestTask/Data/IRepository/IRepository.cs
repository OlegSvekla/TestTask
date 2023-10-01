using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace TestTask.Data.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllByAsync(Func<IQueryable<T>,
            IIncludableQueryable<T, object>> include = null,
            Expression<Func<T, bool>> expression = null,
            CancellationToken cancellationToken = default);

        Task<T> GetOneByAsync(Func<IQueryable<T>,
            IIncludableQueryable<T, object>> include = null,
            Expression<Func<T, bool>> expression = null,
            CancellationToken cancellationToken = default);

        IQueryable<T> GetAllAsync(Func<IQueryable<T>,
            IIncludableQueryable<T, object>>? include = null,
            Expression<Func<T, bool>>? expression = null);
    }
}
