using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SAF.Catalog.Api.Models;

// TODO: Is there any chance for using fluent configuration
// TODO: Resolve null problem
public class Product
{
	[BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	public string Id { get; set; }

	public string Name { get; set; }

	public string Category { get; set; }

	public string Summary { get; set; }

	public string Description { get; set; }

	public string ImageFile { get; set; }

	public decimal Price { get; set; }
}
