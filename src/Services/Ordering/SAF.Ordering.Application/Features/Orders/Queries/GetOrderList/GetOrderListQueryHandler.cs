using AutoMapper;
using MediatR;
using SAF.Ordering.Application.Contracts.Persistance;

namespace SAF.Ordering.Application.Features.Orders.Queries.GetOrderList;

public class GetOrderListQueryHandler : IRequestHandler<GetOrderRequestQuery, List<OrderViewModel>>
{
	private readonly IOrderRepository orderRepository;
	private readonly IMapper mapper;

	public GetOrderListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
	{
		this.orderRepository = orderRepository;
		this.mapper = mapper;
	}

	public async Task<List<OrderViewModel>> Handle(GetOrderRequestQuery request, CancellationToken cancellationToken)
	{
		var result = await orderRepository.GetOrdersByUserName(request.UserName);
		return mapper.Map<List<OrderViewModel>>(result);
	}
}
