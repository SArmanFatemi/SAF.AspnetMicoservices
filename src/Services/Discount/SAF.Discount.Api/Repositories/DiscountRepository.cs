using Dapper;
using SAF.Discount.Api.Models;
using SAF.Discount.Api.Persistence;

namespace SAF.Discount.Api.Repositories;

public class DiscountRepository : IDiscountRepository
{
	private readonly IDatabaseContext databaseContext;

	public DiscountRepository(IDatabaseContext databaseContext)
	{
		this.databaseContext = databaseContext;
		this.databaseContext = databaseContext;
	}
	
	public async Task<Coupon?> Get(string productName)
	{
		var coupon = await databaseContext.Connection.QueryFirstOrDefaultAsync<Coupon?>(
			"SELECT * FROM Coupon WHERE ProductName = @ProductName",
			new { ProductName = productName }
		);

		await databaseContext.Connection.DisposeAsync();

		return coupon;
	}

	public async Task<bool> Create(Coupon coupon)
	{
		var affected =
			await databaseContext.Connection.ExecuteAsync(
				"INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
				new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount }
			);

		await databaseContext.Connection.DisposeAsync();

		return affected is not 0;
	}

	public async Task<bool> Update(Coupon coupon)
	{
		var affected = await databaseContext.Connection.ExecuteAsync(
			"UPDATE Coupon SET ProductName=@ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
			new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount, Id = coupon.Id }
		);

		await databaseContext.Connection.DisposeAsync();

		return affected is not 0;
	}

	public async Task<bool> Delete(string productName)
	{
		var affected = await databaseContext.Connection.ExecuteAsync(
			"DELETE FROM Coupon WHERE ProductName = @ProductName",
			new { ProductName = productName }
		);

		await databaseContext.Connection.DisposeAsync();

		return affected is not 0;
	}
}
