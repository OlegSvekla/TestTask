using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TestTask.Infrastructure.Data.Repository.IRepository;

namespace TestTask.Infrastructure.Data.Repository.Implementation
{
    public class EfRepository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public EfRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllByAsync(Func<IQueryable<T>,
            IIncludableQueryable<T, object>> include = null,
            Expression<Func<T, bool>> expression = null,
            CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbSet;

            if (expression is not null)
            {
                query = query.Where(expression);
            }
            if (include is not null)
            {
                query = include(query);
            }

            return await
                query.AsNoTracking()
                              .ToListAsync(cancellationToken);
        }

        public async Task<T> GetOneByAsync(
                Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                Expression<Func<T, bool>> expression = null,
                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = _dbSet;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (include != null)
            {
                query = include(query);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            var model = await query.AsNoTracking().FirstOrDefaultAsync();
            return model;
        }
    }
}