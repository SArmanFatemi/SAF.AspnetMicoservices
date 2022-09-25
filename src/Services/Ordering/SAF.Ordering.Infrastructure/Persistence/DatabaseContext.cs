using Microsoft.EntityFrameworkCore;
using SAF.Ordering.Domain.Common;
using SAF.Ordering.Domain.Entities;

namespace SAF.Ordering.Infrastructure.Persistence;

public class DatabaseContext : DbContext
{
	public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
	{
	}

	public DbSet<Order> Orders { get; set; } = null!;

	public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		foreach (var entry in ChangeTracker.Entries<BaseEntity>())
		{
			switch (entry.State)
			{
				case EntityState.Added:
					entry.Entity.CreateDateTime = DateTime.Now;
					entry.Entity.CreateBy = "swn";
					break;
				case EntityState.Modified:
					entry.Entity.LastModificationDateTime = DateTime.Now;
					entry.Entity.LastModifyBy = "swn";
					break;
			}
		}

		return base.SaveChangesAsync(cancellationToken);
	}
}
