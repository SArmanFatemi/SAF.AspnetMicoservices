using Microsoft.Extensions.Hosting;
using Npgsql;
using SAF.Discount.Api.Persistence;

namespace SAF.Discount.Api.Repositories;

internal static class ConfigurationServices
{
	public static void AddRepositories(this IServiceCollection services)
	{
		services.AddScoped<IDatabaseContext, DatabaseContext>();
		services.AddScoped<IDiscountRepository, DiscountRepository>();
	}

	public static void UseDatabaseMigration(this WebApplication app, int retry = 0)
	{
		int retryForAvailability = 0;

		using (var scope = app.Services.CreateScope())
		{
			var logger = scope.ServiceProvider.GetRequiredService<ILogger<WebApplication>>();
			var databaseContext = scope.ServiceProvider.GetRequiredService<IDatabaseContext>();

			try
			{
				logger.LogInformation("Migrating postgresql database ...");

				using var connection = databaseContext.GetConnection();
				connection.Open();
				using var command = new NpgsqlCommand
				{
					Connection = connection
				};

				command.CommandText = "DROP TABLE IF EXISTS Coupon";
				command.ExecuteNonQuery();

				command.CommandText = @"
					CREATE TABLE Coupon(
						Id SERIAL PRIMARY KEY,
						ProductName VARCHAR(24) NOT NULL,
						Description TEXT,
						Amount INT
					)";
				command.ExecuteNonQuery();

				command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('IPhone X', 'IPhone Discount', 150);";
				command.ExecuteNonQuery();

				command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Samsung 10', 'Samsung Discount', 100);";
				command.ExecuteNonQuery();

				logger.LogInformation("Migrated postresql database");
			}
			catch (Exception ex)
			{

				logger.LogError(ex, "An error occurred while migrating the postresql database");

				if (retryForAvailability < 50)
				{
					retryForAvailability++;
					Thread.Sleep(2000);
					UseDatabaseMigration(app, retryForAvailability);
				}
			}
		}
	}
}
