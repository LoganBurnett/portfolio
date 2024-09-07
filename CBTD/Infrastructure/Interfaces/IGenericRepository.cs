using Infrastructure.Models;
using System.Linq.Expressions;

namespace Infrastructure.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        // Get an object by its key id
        T GetById(int? id);

        /* The following method will return a set of objects using an Expression filter (similar to 
        a WHERE clause in SQL)
        Func<T, bool> represents a function that takes an object of generic type T and returns a 
        bool on whether filter exists or not*

        Expression<Func<T>> is a description of a function as an expression tree.         
        https://learn.microsoft.com/en-us/dotnet/csharp/advanced-topics/expression-trees/expression-trees-explainedLinks
        The expression is commonly referred to as a predicate and is used to verify a condition on 
        an object.
        https://learn.microsoft.com/en-us/dotnet/api/system.predicate-1?view=net-7.0Links
        The advantage is that Func<T> can be evaluated and compiled at run time and translated to 
        other languages e.g. SQL in LINQ to SQL.

        trackChanges is used to flag whether we want EF to help track changes between read and write 
        (tracking is normally set to true, but adds additional overhead, so if we do not need to track 
        changes, we set to false)

        Includes will be used similarly to a SQL JOIN to “connect and relate to” other objects (PK-FK)
        */
        T Get(Expression<Func<T, bool>> predicate, bool trackChanges = false, string? includes = null);
        
        // Same as Get, but an async call
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool trackChanges = false, string? includes = null);
    
        // Returns an Enumerable list of results to iterate through
        // Expression is the same as before
        // A second expression is added for Order By and column is int
        // Includes allow JOINS
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? predicate = null, Expression<Func<T, int>>? orderBy = null, string? includes = null);

        // Same as GetAll, but async call
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, Expression<Func<T, int>>? orderBy = null, string? includes = null);

        // Add a new instance of the object
        // Doesn't add to the db, it adds it in memory to the object
        void Add(T entity);

        // Delete (Remove) a single instance of an object
        void Delete(T entity);

        // Delete (Remove) a range of items in an object
        void Delete(IEnumerable<T> entities);

        // Update changes to an object
        void Update(T entity);

		// Increment and Decrement Shopping Cart
		int IncrementCount(ShoppingCart shoppingCart, int count);
		int DecrementCount(ShoppingCart shoppingCart, int count);
	}
}
