using AutoMapper;
using SAF.EventBus.Messages.Events;
using SAF.Ordering.Application.Features.Orders.Commands.CheckoutOrder;

namespace SAF.Ordering.Api.Mappings;

public class OrderingProfile : Profile
{
	public OrderingProfile()
	{
		CreateMap<CheckoutOrderCommand, ShoppingCartCheckoutEvent>().ReverseMap();
	}
}
