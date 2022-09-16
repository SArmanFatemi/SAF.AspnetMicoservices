using MongoDB.Driver;
using SAF.Catalog.Api.Models;

namespace SAF.Catalog.Api.Persistence;

internal partial class DatabaseContext
{
	public void ExecuteSeedIfDatabaseIsEmpty()
	{
		IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>()
		{
			new (
				"IPhone X",
				"Smart Phone",
				"This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
				"Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
				"product-1.png",
				950.00M
			),
			new (
				"Samsung 10",
				"Smart Phone",
				"This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
				"Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
				"product-2.png",
				840.00M
			),
			new (
				"Huawei Plus",
				"White Appliances",
				"This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
				"Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
				"product-3.png",
				650.00M
			),
			new (
				"Xiaomi Mi 9",
				"White Appliances",
				"This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
				"Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
				"product-4.png",
				470.00M
			),
			new Product(
				"HTC U11+ Plus",
				"Smart Phone",
				"This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
				"Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
				"product-5.png",
				380.00M
			),
			new (
				"LG G7 ThinQ",
				"Home Kitchen",
				"This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
				"Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
				"product-6.png",
				240.00M
			),
		};


		var isSeededBefore = Products.Find(product => true).Any();
		if (isSeededBefore is false)
		{
			Products.InsertManyAsync(GetPreconfiguredProducts());
		}
	}
}
