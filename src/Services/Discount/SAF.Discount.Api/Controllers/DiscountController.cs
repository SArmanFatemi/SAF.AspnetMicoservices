using Microsoft.AspNetCore.Mvc;
using SAF.Discount.Api.Models;
using SAF.Discount.Api.Repositories;
using System.Net;

namespace SAF.Discount.Api.Controllers;

[Route("api/v1/discount")]
[ApiController]
public class DiscountController : ControllerBase
{
	private readonly IDiscountRepository discountRepository;

	public DiscountController(IDiscountRepository discountRepository)
	{
		this.discountRepository = discountRepository;
	}

	[HttpGet("{productName}", Name = nameof(Get))]
	[ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.NotFound)]
	public async Task<ActionResult<Coupon>> Get(string productName)
	{
		var coupon = await discountRepository.Get(productName);
		return coupon is null ? NotFound() : Ok(coupon);
	}

	[HttpPost]
	[ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
	public async Task<ActionResult<Coupon>> Create([FromBody] Coupon coupon)
	{
		await discountRepository.Create(coupon);
		return CreatedAtRoute(nameof(Get), new { productName = coupon.ProductName }, coupon);
	}

	[HttpPut]
	[ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
	public async Task<ActionResult<Coupon>> Update([FromBody] Coupon coupon) =>
		Ok(await discountRepository.Update(coupon));

	[HttpDelete("{productName}")]
	[ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
	public async Task<ActionResult<bool>> Delete(string productName) =>
		Ok(await discountRepository.Delete(productName));
}
