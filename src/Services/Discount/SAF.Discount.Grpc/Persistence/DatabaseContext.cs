using Microsoft.Extensions.Options;
using Npgsql;
using SAF.Discount.Grpc.Infrastructure.Configuration;

namespace SAF.Discount.Grpc.Persistence;

public class DatabaseContext : IDatabaseContext
{
	private readonly DatabaseSettings databaseSettings;

	public DatabaseContext(IOptions<DatabaseSettings> databaseSettingsOptions)
	{
		databaseSettings = databaseSettingsOptions.Value;
	}

	public NpgsqlConnection GetConnection() => new NpgsqlConnection(databaseSettings.ConnectionString);
}