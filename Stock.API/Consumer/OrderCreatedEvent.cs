using MassTransit;
using MongoDB.Driver;
using Shared;

namespace Stock.API.Consumer
{
    public class OrderCreatedEvent : IConsumer<Shared.OrderCreatedEvent>
    {
        private readonly IMongoCollection<Models.Stock> _collection;

        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderCreatedEvent(ISendEndpointProvider sendEndpointProvider, IPublishEndpoint publishEndpoint, IMongoDbContext context)
        {

            _sendEndpointProvider = sendEndpointProvider;
            _publishEndpoint = publishEndpoint;
            _collection = context.GetCollection();
        }

        public async Task Consume(ConsumeContext<Shared.OrderCreatedEvent> context)
        {
            var stockResult = new List<bool>();

            foreach (var item in context.Message.orderItems)
            {
                stockResult.Add((await _collection.FindAsync(x => x.ProductId == item.ProductId && x.Count >= item.Count)).Any());
            }

            if (stockResult.All(x => x.Equals(true)))
            {
                foreach (var item in context.Message.orderItems)
                {
                    var stock = await _collection.Find(x => x.ProductId == item.ProductId).FirstOrDefaultAsync();

                    if (stock != null)
                    {
                        stock.Count -= item.Count;
                        await _collection.FindOneAndReplaceAsync(x => x.ProductId == item.ProductId, stock);
                    }

                }


                var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMQSettingsConst.StockReservedEventQueue}"));

                StockReservedEvent stockReservedEvent = new StockReservedEvent()
                {
                    Payment = context.Message.Payment,
                    BuyerId = context.Message.BuyerId,
                    OrderId = context.Message.OrderId,
                    OrderItems = context.Message.orderItems
                };

                await sendEndpoint.Send(stockReservedEvent);
            }
            else
            {
                await _publishEndpoint.Publish(new StockNotReservedEvent()
                {
                    OrderId = context.Message.OrderId,
                    Message = "Not enough stock"
                });
            }
        }
    }
}
