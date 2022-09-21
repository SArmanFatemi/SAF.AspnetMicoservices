using AutoMapper;
using Grpc.Core;
using SAF.Discount.Grpc.Models;
using SAF.Discount.Grpc.Protos;
using SAF.Discount.Grpc.Repositories;

namespace SAF.Discount.Grpc.Services
{
	public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
	{
		private readonly IDiscountRepository discountRepository;
		private readonly ILogger<DiscountService> logger;
		private readonly IMapper mapper;

		public DiscountService(IDiscountRepository discountRepository, ILogger<DiscountService> logger, IMapper mapper)
		{
			this.discountRepository = discountRepository;
			this.logger = logger;
			this.mapper = mapper;
		}

		public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
		{
			var coupon = await discountRepository.Get(request.ProductName);
			if (coupon is null)
			{
				throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found."));
			}

			logger.LogInformation("Discount is retrieved for ProductName : {productName}, Amount : {amount}", coupon.ProductName, coupon.Amount);

			return mapper.Map<CouponModel>(coupon);
		}

		public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
		{
			var coupon = mapper.Map<Coupon>(request.Coupon);

			await discountRepository.Create(coupon);
			logger.LogInformation("Discount is successfully created. ProductName : {ProductName}", coupon.ProductName);

			return mapper.Map<CouponModel>(coupon);
		}

		public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
		{
			var coupon = mapper.Map<Coupon>(request.Coupon);

			await discountRepository.Update(coupon);
			logger.LogInformation("Discount is successfully updated. ProductName : {ProductName}", coupon.ProductName);

			return mapper.Map<CouponModel>(coupon);
		}

		public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
		{
			var deleted = await discountRepository.Delete(request.ProductName);
			var response = new DeleteDiscountResponse
			{
				Success = deleted
			};

			return response;
		}
	}
}
