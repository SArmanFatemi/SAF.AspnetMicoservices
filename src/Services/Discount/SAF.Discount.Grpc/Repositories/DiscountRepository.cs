using Dapper;
using SAF.Discount.Grpc.Models;
using SAF.Discount.Grpc.Persistence;

namespace SAF.Discount.Grpc.Repositories;

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
		using var connection = databaseContext.GetConnection();
		var coupon = await connection.QueryFirstOrDefaultAsync<Coupon?>(
			"SELECT * FROM Coupon WHERE ProductName = @ProductName",
			new { ProductName = productName }
		);

		return coupon;
	}

	public async Task<bool> Create(Coupon coupon)
	{
		using var connection = databaseContext.GetConnection();
		var affected =
			await connection.ExecuteAsync(
				"INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
				new { coupon.ProductName, coupon.Description, coupon.Amount }
			);

		return affected is not 0;
	}

	public async Task<bool> Update(Coupon coupon)
	{
		using var connection = databaseContext.GetConnection();
		var affected = await connection.ExecuteAsync(
			"UPDATE Coupon SET ProductName=@ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
			new { coupon.ProductName, coupon.Description, coupon.Amount, coupon.Id }
		);

		return affected is not 0;
	}

	public async Task<bool> Delete(string productName)
	{
		using var connection = databaseContext.GetConnection();

		var affected = await connection.ExecuteAsync(
			"DELETE FROM Coupon WHERE ProductName = @ProductName",
			new { ProductName = productName }
		);

		return affected is not 0;
	}
}