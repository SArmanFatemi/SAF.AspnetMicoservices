using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SAF.Catalog.Api.Infrastructure.Configuration;
using SAF.Catalog.Api.Models;

namespace SAF.Catalog.Api.Persistence;

internal class DatabaseContext : IDatabaseContext
{
	public DatabaseContext(IOptions<DatabaseSettings> databaseSettings)
	{
		var client = new MongoClient(databaseSettings.Value.ConnectionString);
		var database = client.GetDatabase(databaseSettings.Value.Name);

		Products = database.GetCollection<Product>(databaseSettings.Value.CollectionName);
	}

	public IMongoCollection<Product> Products { get; init; }
}
