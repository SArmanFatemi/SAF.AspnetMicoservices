using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SAF.Ordering.Application.Contracts.Infrastructure;
using SAF.Ordering.Application.Contracts.Persistance;
using SAF.Ordering.Application.Models;
using SAF.Ordering.Infrastructure.Persistence;
using SAF.Ordering.Infrastructure.Repositories;

namespace SAF.Ordering.Infrastructure;

public static class ServiceRegistration
{
	public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<DatabaseContext>(options =>
			options.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString")));

		services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));
		services.AddScoped<IOrderRepository, OrderRepository>();

		services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
		services.AddTransient<IEmailService, EmailService>();

		return services;
	}
}
