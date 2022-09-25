using Microsoft.Extensions.Logging;
using SAF.Ordering.Domain.Entities;

namespace SAF.Ordering.Infrastructure.Persistence
{
	internal static class DatabaseContextSeed
	{
		public static async Task SeedAsync(DatabaseContext databaseContext, ILogger<DatabaseContext> logger)
		{
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
				new ()
				{
					UserName = "swn",
					FirstName = "Seyed Arman",
					LastName = "Fatemi",
					EmailAddress = "s.arman.fatemi@gmail.com",
					AddressLine = "Tehran",
					Country = "Iran",
					TotalPrice = 350
			}
			};
		}
	}
}
