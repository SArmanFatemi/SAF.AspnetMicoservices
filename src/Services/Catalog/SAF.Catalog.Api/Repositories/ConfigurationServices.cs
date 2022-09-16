using SAF.Catalog.Api.Persistence;

namespace SAF.Catalog.Api.Repositories;

internal static class ConfigurationServices
{
	public static void AddRepositories(this IServiceCollection services)
	{
		services.AddScoped<IDatabaseContext, DatabaseContext>();

		services.AddScoped<IProductRepository, ProductRepository>();
	}
}
