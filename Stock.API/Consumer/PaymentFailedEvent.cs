using MassTransit;
using MongoDB.Driver;

namespace Stock.API.Consumer
{
    public class PaymentFailedEvent : IConsumer<Shared.PaymentFailedEvent>
    {

        private readonly IMongoDbContext _context;
        private readonly IMongoCollection<Models.Stock> _collection;

        public PaymentFailedEvent(IMongoDbContext context)
        {
            _context = context;
            _collection = _context.GetCollection();
        }

        public async Task Consume(ConsumeContext<Shared.PaymentFailedEvent> context)
        {
            foreach (var item in context.Message.orderItems)
            {
                var stock = await _collection.Find(x => x.ProductId == item.ProductId).FirstOrDefaultAsync();

                if (stock != null)
                {
                    stock.Count += item.Count;
                    await _collection.FindOneAndReplaceAsync(x => x.ProductId == item.ProductId,stock);
                }
            }

        }
    }
}
