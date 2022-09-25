using AutoMapper;
using MediatR;
using SAF.Ordering.Application.Contracts.Persistance;

namespace SAF.Ordering.Application.Features.Orders.Queries.GetOrderList;

public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrderViewModel>>
{
	private readonly IOrderRepository orderRepository;
	private readonly IMapper mapper;

	public GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
	{
		this.orderRepository = orderRepository;
		this.mapper = mapper;
	}

	public async Task<List<OrderViewModel>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
	{
		var result = await orderRepository.GetOrdersByUserName(request.UserName);
		return mapper.Map<List<OrderViewModel>>(result);
	}
}
