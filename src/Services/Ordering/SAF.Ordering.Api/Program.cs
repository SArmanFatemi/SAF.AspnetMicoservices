using SAF.Ordering.Api.Extensions;
using SAF.Ordering.Application;
using SAF.Ordering.Infrastructure;
using SAF.Ordering.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MigrateDatabase<DatabaseContext>(
	(context, services) =>
	{
		var logger = services.GetService<ILogger<DatabaseContext>>();
		DatabaseContextSeed.SeedAsync(context, logger)
			.Wait();
	}
);

app.UseAuthorization();

app.MapControllers();

app.Run();
