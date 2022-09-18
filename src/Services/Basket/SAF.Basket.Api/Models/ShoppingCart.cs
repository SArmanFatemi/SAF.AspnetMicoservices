using System.Text.Json.Serialization;

namespace SAF.Basket.Api.Models;

public class ShoppingCart
{
	[JsonConstructor]
	public ShoppingCart(string username, List<ShoppingCartItem> items)
	{
		Username = username;
		Items = items;
	}

	public ShoppingCart(string username) : this(username, new List<ShoppingCartItem>())
	{
	}

	public string Username { get; set; }

	public List<ShoppingCartItem> Items { get; set; }

	public decimal TotalPrice => Items.Sum(c => c.Price * c.Quantity);
}
