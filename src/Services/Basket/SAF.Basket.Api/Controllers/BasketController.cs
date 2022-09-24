﻿using Microsoft.AspNetCore.Mvc;
using SAF.Basket.Api.GrpcServices;
using SAF.Basket.Api.Models;
using SAF.Basket.Api.Repositories;
using System.Net;

namespace SAF.Basket.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class BasketController : ControllerBase
{
	private readonly IShoppingCartRepository shoppingCartRepository;

	public BasketController(IShoppingCartRepository shoppingCartRepository)
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
	public async Task Delete(string username, CancellationToken cancellationToken) =>
		await shoppingCartRepository.Delete(username, cancellationToken);
}
