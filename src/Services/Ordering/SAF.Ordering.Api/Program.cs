using MassTransit;
using SAF.EventBus.Messages.Common;
using SAF.Ordering.Api.EventBusConsumers;
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
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<ShoppingCardCheckoutConsumer>();
builder.Services.AddMassTransit(config =>
{
	config.AddConsumer<ShoppingCardCheckoutConsumer>();

	config.UsingRabbitMq((context, rabbitmqConfig) => {
		// TODO: Is there any need for strongly type version of this settings?
		rabbitmqConfig.Host(builder.Configuration["EvenetBusSettings:HostAddress"]);

		rabbitmqConfig.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, recieverConfig => {
			recieverConfig.ConfigureConsumer<ShoppingCardCheckoutConsumer>(context);
		});
	});
});

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
