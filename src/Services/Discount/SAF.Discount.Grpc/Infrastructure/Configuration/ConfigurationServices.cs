namespace SAF.Discount.Grpc.Infrastructure.Configuration;

internal static class ConfigurationServices
{
	public static void AddConfigurationServices(this IServiceCollection services, IConfiguration configuration)
	{
		void registerConfiguration<T>()
			where T : class
		{
			services.Configure<T>(configuration.GetSection(typeof(T).Name));
		}

		registerConfiguration<DatabaseSettings>();
	}
}