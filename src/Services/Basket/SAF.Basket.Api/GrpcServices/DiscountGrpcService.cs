using SAF.Discount.Grpc.Protos;

namespace SAF.Basket.Api.GrpcServices;

public class DiscountGrpcService
{
	private readonly DiscountProtoService.DiscountProtoServiceClient discountProtoService;

	public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
	{
		this.discountProtoService = discountProtoService;
	}

	public async Task<CouponModel> Get(string productName) => 
		await discountProtoService.GetDiscountAsync(new() { ProductName = productName });
}
