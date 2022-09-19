using SAF.Discount.Api.Persistence;

namespace SAF.Discount.Api.Repositories;

internal static class ConfigurationServices
{
	public static void AddRepositories(this IServiceCollection services)
	{
		services.AddScoped<IDatabaseContext, DatabaseContext>();
		services.AddScoped<IDiscountRepository, DiscountRepository>();
	}
}
