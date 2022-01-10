using MassTransit;
using Order.API;
using Order.API.Consumers;
using Order.API.Models;
using Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetService<IConfiguration>();
builder.Services.AddControllers();
builder.Services.AddCors();
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
    x.AddConsumer<Order.API.Consumers.PaymentCompletedEvent>();
    x.AddConsumer<Order.API.Consumers.PaymentFailedEvent>();
    x.AddConsumer<Order.API.Consumers.StockNotReservedEvent>();
    x.UsingRabbitMq((context, cfg) =>
    {

        cfg.ReceiveEndpoint(RabbitMQSettingsConst.OrderPaymentCompletedEventQueue, e =>
        {
            e.ConfigureConsumer<Order.API.Consumers.PaymentCompletedEvent>(context);
        });

        cfg.ReceiveEndpoint(RabbitMQSettingsConst.OrderPaymentFailedEventQueue, e =>
        {
            e.ConfigureConsumer<Order.API.Consumers.PaymentFailedEvent>(context);
        });

        cfg.ReceiveEndpoint(RabbitMQSettingsConst.OrderStockNotReservedEventQueue, e =>
        {
            e.ConfigureConsumer<Order.API.Consumers.StockNotReservedEvent>(context);
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
app.UseCors(x => x
           .AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());
app.MapControllers();

app.Run();
