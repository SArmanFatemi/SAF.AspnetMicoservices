using SAF.Ordering.Domain.Common;
using System.Linq.Expressions;

namespace SAF.Ordering.Application.Contracts.Persistance;

public interface IAsyncRepository<T> where T : BaseEntity
{
	Task<IReadOnlyList<T>> GetAllAsync();
	
	Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
	
	Task<IReadOnlyList<T>> GetAsync(
		Expression<Func<T, bool>>? predicate = null,
		Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
		string? includeString = null,
		bool disableTracking = true
	);

	Task<IReadOnlyList<T>> GetAsync(
		Expression<Func<T, bool>>? predicate = null,
		Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
		List<Expression<Func<T, object>>>? includes = null,
		bool disableTracking = true
	);

	Task<T?> GetByIdAsync(int id, bool disableTracking = false);

	Task<T> AddAsync(T entity);

	Task UpdateAsync(T entity);

	Task DeleteAsync(T entity);
}
