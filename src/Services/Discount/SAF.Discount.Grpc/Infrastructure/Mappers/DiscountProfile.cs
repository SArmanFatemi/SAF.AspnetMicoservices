using AutoMapper;
using SAF.Discount.Grpc.Models;
using SAF.Discount.Grpc.Protos;

namespace SAF.Discount.Grpc.Infrastructure.Mappers
{
	public class DiscountProfile : Profile
	{
		public DiscountProfile()
		{
			CreateMap<Coupon, CouponModel>().ReverseMap();
		}
	}
}
