using MassTransit;
using SAF.Basket.Api.GrpcServices;
using SAF.Basket.Api.Infrastructure.Configuration;
using SAF.Basket.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddConfigurationServices(builder.Configuration);
builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddGrpcServices(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMassTransit(config =>
{
	config.UsingRabbitMq((context, rabbitmqConfig) => {
		// TODO: Is there any need for strongly type version of this settings?
		rabbitmqConfig.Host(builder.Configuration["EvenetBusSettings:HostAddress"]);

	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
