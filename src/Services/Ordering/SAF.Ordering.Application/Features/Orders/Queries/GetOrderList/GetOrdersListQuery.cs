using MediatR;

namespace SAF.Ordering.Application.Features.Orders.Queries.GetOrderList;

public class GetOrdersListQuery : IRequest<List<OrderViewModel>>
{
	public string UserName { get; set; }

	public GetOrdersListQuery(string userName)
	{
		UserName = userName;
	}
}
