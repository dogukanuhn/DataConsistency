using MassTransit;
using MongoDB.Driver;
using Order.API.Models;
using Shared;

namespace Order.API.Consumers
{
    public class PaymentCompletedEvent: IConsumer<Shared.PaymentCompletedEvent>
    {
        private readonly IMongoDbContext _context;
        private readonly IMongoCollection<Models.Order> _collection;


        public PaymentCompletedEvent( IMongoDbContext context)
        {
            _context=context;
            _collection = _context.GetCollection(); ;
        }

        public async Task Consume(ConsumeContext<Shared.PaymentCompletedEvent> context)
        {
            var order = await  _collection.Find(x => x.OrderId == context.Message.orderId).FirstOrDefaultAsync();

            if (order != null)
            {
                order.Status = OrderStatus.Complete;
                await _collection.FindOneAndReplaceAsync(x => x.OrderId == context.Message.orderId, order);

            }
            
        }
    }
}
