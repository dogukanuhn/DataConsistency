using Stock.API;
using Stock.API.Models;
using MassTransit;
using Stock.API.Consumer;
using Shared;

var builder = WebApplication.CreateBuilder(args);

var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetService<IConfiguration>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMongoDbContext, DbContext>();

builder.Services.Configure<MongoDbSettings>(options =>
{
    options.ConnectionString = configuration
        .GetSection(nameof(MongoDbSettings) + ":" + MongoDbSettings.ConnectionStringValue).Value;
    options.Database = configuration
        .GetSection(nameof(MongoDbSettings) + ":" + MongoDbSettings.DatabaseValue).Value;
});

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<Stock.API.Consumer.OrderCreatedEvent>();

    x.AddConsumer<Stock.API.Consumer.PaymentFailedEvent>();
    x.UsingRabbitMq((context, cfg) =>
    {
        //cfg.Host("rabbitmq");
        cfg.ReceiveEndpoint(RabbitMQSettingsConst.StockOrderCreatedEventQueue, e =>
        {
            e.ConfigureConsumer<Stock.API.Consumer.OrderCreatedEvent>(context);
        });

        cfg.ReceiveEndpoint(RabbitMQSettingsConst.StockPaymentFailedEventQueue, e =>
        {
            e.ConfigureConsumer<Stock.API.Consumer.PaymentFailedEvent>(context);
        });
    });
});
builder.Services.AddMassTransitHostedService();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
