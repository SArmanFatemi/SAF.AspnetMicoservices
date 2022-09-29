using Microsoft.EntityFrameworkCore;
using SAF.Ordering.Application.Contracts.Persistance;
using SAF.Ordering.Domain.Common;
using SAF.Ordering.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace SAF.Ordering.Infrastructure.Repositories
{
	public class AsyncRepository<T> : IAsyncRepository<T> where T : BaseEntity
	{
		protected readonly DatabaseContext databaseContext;

		public AsyncRepository(DatabaseContext databaseContext)
		{
			this.databaseContext = databaseContext;
		}

		public async Task<IReadOnlyList<T>> GetAllAsync() =>
			await databaseContext.Set<T>().ToListAsync();

		public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate) =>
			await databaseContext.Set<T>().Where(predicate).ToListAsync();

		public async Task<IReadOnlyList<T>> GetAsync(
			Expression<Func<T, bool>>? predicate,
			Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
			string? includeString,
			bool disableTracking = true)
		{
			var query = databaseContext.Set<T>().AsQueryable<T>();
			if (disableTracking) query = query.AsNoTracking();

			if (string.IsNullOrWhiteSpace(includeString) is false) query = query.Include(includeString);

			if (predicate is not null) query = query.Where(predicate);

			if (orderBy is not null)
				return await orderBy(query).ToListAsync();

			return await query.ToListAsync();
		}

		public async Task<IReadOnlyList<T>> GetAsync(
			Expression<Func<T, bool>>? predicate,
			Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
			List<Expression<Func<T, object>>>? includes,
			bool disableTracking = true)
		{
			var query = databaseContext.Set<T>().AsQueryable<T>();

			if (disableTracking) query = query.AsNoTracking();

			if (includes is not null) query = includes.Aggregate(query, (current, include) => current.Include(include));

			if (predicate is not null) query = query.Where(predicate);

			if (orderBy is not null)
				return await orderBy(query).ToListAsync();

			return await query.ToListAsync();
		}

		public virtual async Task<T?> GetByIdAsync(int id, bool disableTracking = false)
		{
			var query = databaseContext.Set<T>().AsQueryable<T>();
			if (disableTracking) query = query.AsNoTracking();

			return await query.SingleOrDefaultAsync(current => current.Id == id);
		}

		public async Task<T> AddAsync(T entity)
		{
			databaseContext.Set<T>().Add(entity);
			await databaseContext.SaveChangesAsync();

			return entity;
		}

		public async Task UpdateAsync(T entity)
		{
			databaseContext.Entry(entity).State = EntityState.Modified;
			await databaseContext.SaveChangesAsync();
		}

		public async Task DeleteAsync(T entity)
		{
			databaseContext.Set<T>().Remove(entity);
			await databaseContext.SaveChangesAsync();
		}
	}
}
