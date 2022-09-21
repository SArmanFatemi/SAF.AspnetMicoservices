using SAF.Discount.Grpc.Infrastructure.Configuration;
using SAF.Discount.Grpc.Repositories;
using SAF.Discount.Grpc.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddConfigurationServices(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

app.UseDatabaseMigration();

// Configure the HTTP request pipeline.
app.MapGrpcService<DiscountService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
