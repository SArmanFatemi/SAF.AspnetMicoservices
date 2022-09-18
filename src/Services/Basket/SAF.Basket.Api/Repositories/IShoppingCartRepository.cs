using SAF.Basket.Api.Models;

namespace SAF.Basket.Api.Repositories;

public interface IShoppingCartRepository
{
	Task<Models.ShoppingCart?> Get(string username, CancellationToken cancellationToken);

	Task<Models.ShoppingCart> Update(Models.ShoppingCart shoppingCart, CancellationToken cancellationToken);

	Task Delete(string username, CancellationToken cancellationToken);
}
