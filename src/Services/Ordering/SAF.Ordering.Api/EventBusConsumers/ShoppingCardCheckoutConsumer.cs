using AutoMapper;
using MassTransit;
using MediatR;
using SAF.EventBus.Messages.Events;
using SAF.Ordering.Application.Features.Orders.Commands.CheckoutOrder;

namespace SAF.Ordering.Api.EventBusConsumers;

public class ShoppingCardCheckoutConsumer : IConsumer<ShoppingCartCheckoutEvent>
{
	private readonly IMapper mapper;
	private readonly IMediator mediator;
	private readonly ILogger<ShoppingCardCheckoutConsumer> logger;

	public ShoppingCardCheckoutConsumer(
		IMapper mapper,
		IMediator mediator,
		ILogger<ShoppingCardCheckoutConsumer> logger
	)
	{
		this.mapper = mapper;
		this.mediator = mediator;
		this.logger = logger;
	}

	public async Task Consume(ConsumeContext<ShoppingCartCheckoutEvent> context)
	{
		var command = mapper.Map<CheckoutOrderCommand>(context.Message);
		var result = await mediator.Send(command);

		logger.LogInformation("BasketCheckoutEvent consumed successfully. Created Order Id : {newOrderId}", result);
	}
}
