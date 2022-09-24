using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SAF.Ordering.Application.Contracts.Infrastructure;
using SAF.Ordering.Application.Contracts.Persistance;
using SAF.Ordering.Application.Models;
using SAF.Ordering.Domain.Entities;
using System.Runtime.InteropServices;

namespace SAF.Ordering.Application.Features.Orders.Commands.CheckoutOrder;

public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
{
	private readonly IOrderRepository orderRepository;
	private readonly IMapper mapper;
	private readonly IEmailService emailService;
	private readonly ILogger<CheckoutOrderCommandHandler> logger;

	public CheckoutOrderCommandHandler(
		IOrderRepository orderRepository,
		IMapper mapper,
		IEmailService emailService,
		ILogger<CheckoutOrderCommandHandler> logger)
	{
		this.orderRepository = orderRepository;
		this.mapper = mapper;
		this.emailService = emailService;
		this.logger = logger;
	}

	public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
	{
		var orderEntity = mapper.Map<Order>(request);
		var newOrder = await orderRepository.AddAsync(orderEntity);

		logger.LogInformation($"Order {newOrder.Id} is successfully created");

		await SendEmail(newOrder);

		return newOrder.Id;
	}

	private async Task SendEmail(Order order)
	{
		var email = new Email { To = "s.arman.fatemi@outlook.com", Body="Order was created", Subject = "Order was created" };
		try
		{
			await emailService.SendEmail(email);
		}
		catch (Exception ex)
		{

			logger.LogError($"Order {order.Id} faild due to an error with the mail service: ${ex.Message}");
		}
	}
}
