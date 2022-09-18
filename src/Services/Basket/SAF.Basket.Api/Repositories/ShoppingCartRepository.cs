using Microsoft.Extensions.Caching.Distributed;
using SAF.Basket.Api.Models;
using System.Text.Json;

namespace SAF.Basket.Api.Repositories;

public class ShoppingCartRepository : IShoppingCartRepository
{
	private readonly IDistributedCache distributedCache;

	public ShoppingCartRepository(IDistributedCache distributedCache)
	{
		this.distributedCache = distributedCache;
	}

	public async Task<ShoppingCart?> Get(string username, CancellationToken cancellationToken)
	{
		var rawShoppingCart = await distributedCache.GetStringAsync(username, cancellationToken);

		if (string.IsNullOrEmpty(rawShoppingCart)) return null;

		return JsonSerializer.Deserialize<ShoppingCart>(rawShoppingCart);
	}

	public async Task<ShoppingCart> Update(ShoppingCart shoppingCart, CancellationToken cancellationToken)
	{
		await distributedCache.SetStringAsync(shoppingCart.Username, JsonSerializer.Serialize(shoppingCart), cancellationToken);
		var shoppingCartFromDatabase = await Get(shoppingCart.Username, cancellationToken);
		if (shoppingCartFromDatabase is null)
		{
			throw new NullReferenceException();
		}

		return shoppingCart;
	}

	public async Task Delete(string username, CancellationToken cancellationToken) =>
		await distributedCache.RemoveAsync(username, cancellationToken);
}
