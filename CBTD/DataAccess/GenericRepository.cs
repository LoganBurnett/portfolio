using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        // By using ReadOnly ApplicationDbContext, you can have have access to only querying
        // capablities of DbContext. UnitOfWork actually writes (commits) to the PHYSICAL tables
        // (not internal objects)
        private readonly ApplicationDbContext _dbContext;
        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public void Delete(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
        }

        public virtual T Get(Expression<Func<T, bool>> predicate, bool trackChanges = false, string? includes = null)
        {
            IQueryable<T> queryable = _dbContext.Set<T>();

            if (!string.IsNullOrEmpty(includes)) // If other objects to include (join)
            {
                foreach (var includeProperty in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    queryable = queryable.Include(includeProperty);
                }
            }

            if (!trackChanges) // If we do not want EF tracking changes
            {
                queryable = queryable.AsNoTracking();
            }

            return queryable.FirstOrDefault(predicate);
        }

        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool trackChanges = false, string? includes = null)
        {
            IQueryable<T> queryable = _dbContext.Set<T>();

            if (!string.IsNullOrEmpty(includes)) // If other objects to include (join)
            {
                var includeProperties = includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var includeProperty in includeProperties)
                {
                    queryable = queryable.Include(includeProperty);
                }
            }

            if (!trackChanges) // If we do not want EF tracking changes
            {
                queryable = queryable.AsNoTracking();
            }

            return await queryable.FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? predicate = null, Expression<Func<T, int>>? orderBy = null, string? includes = null)
        {
            IQueryable<T> queryable = _dbContext.Set<T>();

            if (!string.IsNullOrEmpty(includes))
            {
                foreach (var includeProperty in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    queryable = queryable.Include(includeProperty);
                }
            }

            if (predicate != null)
            {
                queryable = queryable.Where(predicate);
            }

            if (orderBy != null)
            {
                queryable = queryable.OrderBy(orderBy);
            }

            return queryable.ToList();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, Expression<Func<T, int>>? orderBy = null, string? includes = null)
        {
            IQueryable<T> queryable = _dbContext.Set<T>();

            if (!string.IsNullOrEmpty(includes))
            {
                foreach (var includeProperty in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    queryable = queryable.Include(includeProperty);
                }
            }

            if (predicate != null)
            {
                queryable = queryable.Where(predicate);
            }

            if (orderBy != null)
            {
                queryable = queryable.OrderBy(orderBy);
            }

            return await queryable.ToListAsync();
        }


        // The virtual keyword is used to modify a method, property, or indexer and allows it to be
        // overridden in a derived class
        public virtual T GetById(int? id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
            // For track changes, I'm flagging modified to the system
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

		public int DecrementCount(ShoppingCart shoppingCart, int count)
		{
			shoppingCart.Count -= count;
			return shoppingCart.Count;
		}

		public int IncrementCount(ShoppingCart shoppingCart, int count)
		{
			shoppingCart.Count += count;
			return shoppingCart.Count;
		}

	}
}
