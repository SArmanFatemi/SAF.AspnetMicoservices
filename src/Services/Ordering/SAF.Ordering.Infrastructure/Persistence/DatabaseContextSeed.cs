using Microsoft.Extensions.Logging;
using SAF.Ordering.Domain.Entities;
using System.Xml.Linq;

namespace SAF.Ordering.Infrastructure.Persistence
{
	public static class DatabaseContextSeed
	{
		public static async Task SeedAsync(DatabaseContext databaseContext, ILogger<DatabaseContext>? logger)
		{
			ArgumentNullException.ThrowIfNull(logger);

			if (databaseContext.Orders.Any() is false)
			{
				await databaseContext.Orders.AddRangeAsync(GetPreconfiguredOrders());
				await databaseContext.SaveChangesAsync();

				logger.LogInformation("Seed database associated with context {DbContextName}", typeof(DatabaseContext).Name);
			}
		}

		private static IEnumerable<Order> GetPreconfiguredOrders()
		{
			return new List<Order>
			{
				new ("swn", 350, "Seyed Arman", "Fatemi", "s.arman.fatemi@gmail.com", "Iran",  "Tehran", "Tehran Pars")
			};
		}
	}
}
