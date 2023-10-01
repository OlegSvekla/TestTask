using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using TestTask.Data.IRepository;
using System.Runtime.CompilerServices;

namespace TestTask.Data
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

        //public async IQueryable<T> GetAllAsync(Func<IQueryable<T>,
        //    IIncludableQueryable<T, object>>? include = null,
        //    Expression<Func<T, bool>>? expression = null)
        //{
        //    IQueryable<T> query = _dbSet;

        //    if (expression is not null)
        //    {
        //        query = query.Where(expression);
        //    }
        //    if (include is not null)
        //    {
        //        query = include(query);
        //    }

        //    return await query.AsNoTracking().ToListAsync();
        //}

        public IQueryable<T> GetAllAsync(Func<IQueryable<T>,
            IIncludableQueryable<T, object>>? include = null,
            Expression<Func<T, bool>>? expression = null)
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

            return query.AsNoTracking();
        }

        //public IQueryable<T> GetAllBy(Func<IQueryable<T>, IQueryable<T>> include = null, Expression<Func<T, bool>> expression = null)
        //{
        //    IQueryable<T> query = _dbSet;

        //    if (expression is not null)
        //    {
        //        query = query.Where(expression);
        //    }
        //    if (include is not null)
        //    {
        //        query = include(query);
        //    }

        //    return query.AsNoTracking();
        //}

        public async Task<T> GetOneByAsync(Func<IQueryable<T>,
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

            var model = await query.AsNoTracking()
                                   .FirstOrDefaultAsync(cancellationToken);

            return model;
        }
    }
}