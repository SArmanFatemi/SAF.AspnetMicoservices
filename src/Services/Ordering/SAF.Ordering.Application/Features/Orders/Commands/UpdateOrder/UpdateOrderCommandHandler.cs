using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SAF.Ordering.Application.Contracts.Persistance;
using SAF.Ordering.Application.Exceptions;
using SAF.Ordering.Domain.Entities;

namespace SAF.Ordering.Application.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
{
	private readonly IOrderRepository orderRepository;
	private readonly IMapper mapper;
	private readonly ILogger<UpdateOrderCommandHandler> logger;

	public UpdateOrderCommandHandler(
		IOrderRepository orderRepository,
		IMapper mapper,
		ILogger<UpdateOrderCommandHandler> logger)
	{
		this.orderRepository = orderRepository;
		this.mapper = mapper;
		this.logger = logger;
	}

	public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
	{
		var orderToUpdate = await orderRepository.GetByIdAsync(request.Id, true);
		if (orderToUpdate is null)
		{
			throw new XNotFoundException(nameof(Order), request.Id);
		}

		orderToUpdate = mapper.Map<Order>(request);
		await orderRepository.UpdateAsync(orderToUpdate);
		logger.LogInformation($"Order {orderToUpdate.Id} is successfully updated");

		return Unit.Value;
	}
}
