using Microsoft.AspNetCore.Mvc;
using SAF.Catalog.Api.Models;
using SAF.Catalog.Api.Repositories;
using System.Net;

namespace SAF.Catalog.Api.Controllers;

[ApiController]
[Route("api/v1/product")]
public class ProductController : ControllerBase
{
	private readonly ILogger<ProductController> logger;
	private readonly IProductRepository productRepository;

	public ProductController(ILogger<ProductController> logger, IProductRepository productRepository)
	{
		this.logger = logger;
		this.productRepository = productRepository;
	}

	[HttpGet]
	[ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
	public async Task<ActionResult<IEnumerable<Product>>> GetAll(CancellationToken cancellationToken) =>
		Ok(await productRepository.GetAll(cancellationToken));

	[HttpGet("{id:length(24)}")]
	[ProducesResponseType((int)HttpStatusCode.NotFound)]
	[ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
	public async Task<ActionResult<Product>> GetById(string id, CancellationToken cancellationToken)
	{
		var product = await productRepository.GetById(id, cancellationToken);

		if (product is null)
		{
			logger.LogWarning($"Product with id: {id}, not found.");
			return NotFound();
		}

		return Ok(product);
	}

	[HttpGet("category/{category}")]
	[ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
	public async Task<ActionResult<IEnumerable<Product>>> GetByCategory(string category, CancellationToken cancellationToken) =>
		Ok(await productRepository.GetByCategory(category, cancellationToken));

	[HttpGet("name/{name}")]
	[ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
	public async Task<ActionResult<IEnumerable<Product>>> GetByName(string name, CancellationToken cancellationToken) =>
		Ok(await productRepository.GetByName(name, cancellationToken));

	[HttpPost]
	[ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
	public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product, CancellationToken cancellationToken)
	{
		await productRepository.Create(product, cancellationToken);

		return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
	}

	[HttpPut]
	[ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
	public async Task<IActionResult> Update([FromBody] Product product, CancellationToken cancellationToken) =>
		Ok(await productRepository.Update(product, cancellationToken));

	[HttpDelete("{id:length(24)}")]
	[ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
	public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken) =>
		Ok(await productRepository.Delete(id, cancellationToken));
}
