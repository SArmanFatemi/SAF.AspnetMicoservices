
namespace SAF.Basket.Api.Repositories;

internal static class ConfigurationServices
{
	public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddStackExchangeRedisCache(options =>
		{
			// TODO: Strongly type
			options.Configuration = configuration.GetValue<string>("CacheSettings:ConnectionString");
		});

		services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
	}
}
