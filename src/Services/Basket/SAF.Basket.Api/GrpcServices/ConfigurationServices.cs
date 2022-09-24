using SAF.Basket.Api.Infrastructure.Configuration;
using SAF.Basket.Api.Repositories;
using SAF.Discount.Grpc.Protos;

namespace SAF.Basket.Api.GrpcServices;

internal static class ConfigurationServices1
{
	public static void AddGrpcServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(o => o.Address = new Uri(configuration[$"{nameof(GrpcSettings)}:{nameof(GrpcSettings.DiscountUrl)}"]));
		services.AddScoped<DiscountGrpcService>();
	}
}
