using MassTransit;
using MongoDB.Driver;
using Order.API.Models;

namespace Order.API.Consumers
{
    public class PaymentFailedEvent : IConsumer<Shared.PaymentFailedEvent>
    {
        private readonly IMongoDbContext _context;
        private readonly IMongoCollection<Models.Order> _collection;


        public PaymentFailedEvent(IMongoDbContext context)
        {
            _context = context;
            _collection = _context.GetCollection(); 
        }

     
        public async Task Consume(ConsumeContext<Shared.PaymentFailedEvent> context)
        {
            var order = await _collection.Find(x => x.OrderId == context.Message.orderId).FirstOrDefaultAsync();

            if (order != null)
            {
                order.Status = OrderStatus.Fail;
                order.FailMessage = context.Message.Message;
                await _collection.FindOneAndReplaceAsync(x => x.OrderId == context.Message.orderId, order);
            }
        }
    }
}
