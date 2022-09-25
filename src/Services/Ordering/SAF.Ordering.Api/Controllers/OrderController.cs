using MediatR;
using Microsoft.AspNetCore.Mvc;
using SAF.Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using SAF.Ordering.Application.Features.Orders.Commands.DeleteOrder;
using SAF.Ordering.Application.Features.Orders.Commands.UpdateOrder;
using SAF.Ordering.Application.Features.Orders.Queries.GetOrderList;
using System.Net;

namespace SAF.Ordering.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IMediator mediator;

		public OrderController(IMediator mediator)
		{
			this.mediator = mediator;
		}

		[HttpGet("{userName}")]
		[ProducesResponseType(typeof(IEnumerable<OrderViewModel>), (int)HttpStatusCode.OK)]
		public async Task<ActionResult<IEnumerable<OrderViewModel>>> GetOrdersByUserName(string userName) =>
			Ok(await mediator.Send(new GetOrdersListQuery(userName)));

		// For testing purpose
		[HttpPost]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		public async Task<ActionResult<int>> CheckoutOrder([FromBody] CheckoutOrderCommand command) =>
			Ok(await mediator.Send(command));

		[HttpPut]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<ActionResult> UpdateOrder([FromBody] UpdateOrderCommand command)
		{
			await mediator.Send(command);
			return NoContent();
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<ActionResult> DeleteOrder(int id)
		{
			await mediator.Send(new DeleteOrderCommand() { Id = id });
			return NoContent();
		}
	}
}
