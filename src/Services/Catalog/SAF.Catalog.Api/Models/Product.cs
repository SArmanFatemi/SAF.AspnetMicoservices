using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SAF.Catalog.Api.Models;

// TODO: Is there any chance for using fluent configuration
public class Product
{
	public Product(string name, string category, string summary, string description, string imageFile, decimal price)
	{
		Id = ObjectId.GenerateNewId().ToString();
		Name = name;
		Category = category;
		Summary = summary;
		Description = description;
		ImageFile = imageFile;
		Price = price;
	}

	[BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	public string Id { get; private set; }


	public string Name { get; private set; }

	public string Category { get; private set; }

	public string Summary { get; private set; }

	public string Description { get; private set; }

	public string ImageFile { get; private set; }

	public decimal Price { get; private set; }
}
