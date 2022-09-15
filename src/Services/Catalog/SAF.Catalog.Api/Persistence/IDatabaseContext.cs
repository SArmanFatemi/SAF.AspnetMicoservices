using MongoDB.Driver;
using SAF.Catalog.Api.Models;

namespace SAF.Catalog.Api.Persistence;

internal interface IDatabaseContext
{
	IMongoCollection<Product> Products { get; }
}
