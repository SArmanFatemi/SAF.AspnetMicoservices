namespace SAF.Catalog.Api.Infrastructure.Configuration;

public class DatabaseSettings
{
	public string? ConnectionString { get; init; }

	public string? Name { get; init; }

	public string? CollectionName { get; init; }
}
