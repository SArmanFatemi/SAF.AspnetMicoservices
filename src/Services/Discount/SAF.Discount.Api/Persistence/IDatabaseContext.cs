using Npgsql;

namespace SAF.Discount.Api.Persistence;

public interface IDatabaseContext
{
	NpgsqlConnection GetConnection();
}