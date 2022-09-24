﻿using AutoMapper;
using SAF.Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using SAF.Ordering.Application.Features.Orders.Queries.GetOrderList;
using SAF.Ordering.Domain.Entities;

namespace SAF.Ordering.Application.Mappings;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<Order, OrderViewModel>().ReverseMap();
		CreateMap<Order, CheckoutOrderCommand>().ReverseMap();
	}
}
