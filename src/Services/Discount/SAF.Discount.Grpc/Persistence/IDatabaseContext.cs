using Npgsql;

namespace SAF.Discount.Grpc.Persistence;

public interface IDatabaseContext
{
	NpgsqlConnection GetConnection();
}