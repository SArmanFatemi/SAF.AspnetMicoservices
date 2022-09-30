using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using SAF.Basket.Api.GrpcServices;
using SAF.Basket.Api.Models;
using SAF.Basket.Api.Repositories;
using SAF.EventBus.Messages.Events;
using System.Net;

namespace SAF.Basket.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ShoppingCartController : ControllerBase
{
	private readonly IShoppingCartRepository shoppingCartRepository;

	public ShoppingCartController(IShoppingCartRepository shoppingCartRepository)
	{
		this.shoppingCartRepository = shoppingCartRepository;
	}

	[HttpGet("{username}")]
	[ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
	public async Task<ActionResult<ShoppingCart>> Get(string username, CancellationToken cancellationToken)
	{
		var shoppingCart = await shoppingCartRepository.Get(username, cancellationToken);
		return base.Ok(shoppingCart ?? new ShoppingCart(username));
	}

	[HttpPost]
	[ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
	public async Task<ActionResult<ShoppingCart>> Update(
		[FromServices] DiscountGrpcService discountGrpcService,
		[FromBody] ShoppingCart shoppingCart,
		CancellationToken cancellationToken)
	{
		foreach (var item in shoppingCart.Items)
		{
			var coupon = await discountGrpcService.Get(item.ProductName);
			if (coupon is not null) item.Price -= coupon.Amount;
		}

		return Ok(await shoppingCartRepository.Update(shoppingCart, cancellationToken));
	}

	[HttpDelete("{username}")]
	[ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
	public async Task Delete(string username, CancellationToken cancellationToken) =>
		await shoppingCartRepository.Delete(username, cancellationToken);

	[HttpPost("checkout")]
	[ProducesResponseType((int)HttpStatusCode.Accepted)]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	public async Task<IActionResult> Checkout(
		[FromServices] IMapper mapper,
		[FromServices] IPublishEndpoint publishEndpoint,
		[FromBody] ShoppingCartCheckout shoppingCartCheckout,
		CancellationToken cancellationToken)
	{
		var shoppingCart = await shoppingCartRepository.Get(shoppingCartCheckout.UserName, cancellationToken);
		if (shoppingCart is null)
		{
			return BadRequest();
		}

		var eventMessage = mapper.Map<ShoppingCartCheckoutEvent>(shoppingCartCheckout);

		eventMessage.TotalPrice = shoppingCart.TotalPrice;
		await publishEndpoint.Publish(eventMessage);

		await shoppingCartRepository.Delete(shoppingCartCheckout.UserName, cancellationToken);

		return Accepted();
	}
}
