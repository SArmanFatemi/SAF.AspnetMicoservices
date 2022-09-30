using AutoMapper;
using SAF.Basket.Api.Models;
using SAF.EventBus.Messages.Events;

namespace SAF.Basket.Api.Mapper;

public class ShoppingCartProfile : Profile
{
	public ShoppingCartProfile()
	{
		CreateMap<ShoppingCartCheckout, ShoppingCartCheckoutEvent>().ReverseMap();
	}
}