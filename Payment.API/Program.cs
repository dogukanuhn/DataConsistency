using MassTransit;
using Payment.API.Consumers;
using Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetService<IConfiguration>();





builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<StockReservedEventConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        //cfg.Host("rabbitmq");
        cfg.ReceiveEndpoint(RabbitMQSettingsConst.StockReservedEventQueue, e =>
        {
            e.ConfigureConsumer<StockReservedEventConsumer>(context);
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
