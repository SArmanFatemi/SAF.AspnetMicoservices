using Microsoft.Extensions.Options;
using Npgsql;
using SAF.Discount.Api.Infrastructure.Configuration;

namespace SAF.Discount.Api.Persistence;

public class DatabaseContext : IDatabaseContext
{
	private readonly DatabaseSettings databaseSettings;

	public DatabaseContext(IOptions<DatabaseSettings> databaseSettingsOptions)
	{
		this.databaseSettings = databaseSettingsOptions.Value;
	}

	public NpgsqlConnection GetConnection() => new NpgsqlConnection(databaseSettings.ConnectionString);
}
