using MediatR;

namespace SAF.Ordering.Application.Features.Orders.Queries.GetOrderList;

public class GetOrderRequestQuery : IRequest<List<OrderViewModel>>
{
	public string UserName { get; set; }

	public GetOrderRequestQuery(string userName)
	{
		UserName = userName;
	}
}
