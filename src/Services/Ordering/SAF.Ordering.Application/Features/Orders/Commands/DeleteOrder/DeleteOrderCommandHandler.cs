using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SAF.Ordering.Application.Contracts.Persistance;
using SAF.Ordering.Application.Exceptions;
using SAF.Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAF.Ordering.Application.Features.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
{
	private readonly IOrderRepository orderRepository;
	private readonly IMapper mapper;
	private readonly ILogger<DeleteOrderCommandHandler> logger;

	public DeleteOrderCommandHandler(
		IOrderRepository orderRepository,
		IMapper mapper,
		ILogger<DeleteOrderCommandHandler> logger)
	{
		this.orderRepository = orderRepository;
		this.mapper = mapper;
		this.logger = logger;
	}


	public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
	{
		var orderToDelete = await orderRepository.GetByIdAsync(request.Id);
		if(orderToDelete is null)
		{
			throw new XNotFoundException(nameof(Order), request.Id);
		}

		await orderRepository.DeleteAsync(orderToDelete);
		logger.LogInformation($"Order {orderToDelete.Id} is successfully deleted");

		return Unit.Value;
	}
}
