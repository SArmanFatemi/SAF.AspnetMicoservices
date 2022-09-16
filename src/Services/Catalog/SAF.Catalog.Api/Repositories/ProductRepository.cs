using MongoDB.Driver;
using SAF.Catalog.Api.Models;
using SAF.Catalog.Api.Persistence;

namespace SAF.Catalog.Api.Repositories;

internal class ProductRepository : IProductRepository
{
	private readonly IDatabaseContext databaseContext;

	public ProductRepository(IDatabaseContext databaseContext)
	{
		this.databaseContext = databaseContext;
	}

	public async Task<Product> GetById(string id, CancellationToken cancellationToken) => await databaseContext.Products
		.Find(p => p.Id == id)
		.SingleOrDefaultAsync(cancellationToken);

	public async Task<IEnumerable<Product>> GetAll(CancellationToken cancellationToken) => await databaseContext.Products
		.Find(p => true)
		.ToListAsync(cancellationToken);

	public async Task<IEnumerable<Product>> GetByCategory(string category, CancellationToken cancellationToken)
	{
		var filter = Builders<Product>.Filter.Eq(p => p.Category, category);

		return await databaseContext.Products
			.Find(filter)
			.ToListAsync(cancellationToken);
	}


	public async Task<IEnumerable<Product>> GetByName(string name, CancellationToken cancellationToken)
	{
		var filter = Builders<Product>.Filter.Eq(p => p.Name, name);

		return await databaseContext.Products
			.Find(filter)
			.ToListAsync(cancellationToken);
	}

	public async Task Create(Product product, CancellationToken cancellationToken)
	{
		await databaseContext.Products.InsertOneAsync(product, cancellationToken: cancellationToken);
	}

	public async Task<bool> Delete(string id, CancellationToken cancellationToken)
	{
		var filter = Builders<Product>.Filter.Eq(p => p.Id, id);

		var result = await databaseContext.Products.DeleteOneAsync(filter, cancellationToken);

		return result.IsAcknowledged && result.DeletedCount > 0;
	}

	public async Task<bool> Update(Product product, CancellationToken cancellationToken)
	{
		var result = await databaseContext.Products
			.ReplaceOneAsync(p => p.Id.Equals(product.Id), product, cancellationToken: cancellationToken);

		return result.IsAcknowledged && result.ModifiedCount > 0;
	}
}
