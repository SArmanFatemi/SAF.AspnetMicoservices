using SAF.Catalog.Api.Models;

namespace SAF.Catalog.Api.Repositories;

public interface IProductRepository
{
	Task<IEnumerable<Product>> GetAll(CancellationToken cancellationToken);

	Task<Product> GetById(string id, CancellationToken cancellationToken);

	Task<IEnumerable<Product>> GetByName(string name, CancellationToken cancellationToken);

	Task<IEnumerable<Product>> GetByCategory(string category, CancellationToken cancellationToken);

	Task Create(Product product, CancellationToken cancellationToken);

	Task<bool> Update(Product product, CancellationToken cancellationToken);

	Task<bool> Delete(string id, CancellationToken cancellationToken);
}
