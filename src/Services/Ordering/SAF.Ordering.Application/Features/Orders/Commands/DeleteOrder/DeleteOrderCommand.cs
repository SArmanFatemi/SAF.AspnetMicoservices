using MediatR;

namespace SAF.Ordering.Application.Features.Orders.Commands.DeleteOrder;

public class DeleteOrderCommand : IRequest
{
	public int Id { get; set; }
}
