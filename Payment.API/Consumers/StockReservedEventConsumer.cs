using MassTransit;
using Shared;

namespace Payment.API.Consumers

{
    public class StockReservedEventConsumer : IConsumer<Shared.StockReservedEvent>
    {


        private readonly IPublishEndpoint _publishEndpoint;

        public StockReservedEventConsumer( IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<StockReservedEvent> context)
        {
            var balance = 3000m;

            if (balance > context.Message.Payment.TotalPrice)
            {
            
                await _publishEndpoint.Publish(new PaymentCompletedEvent { BuyerId = context.Message.BuyerId, orderId = context.Message.OrderId });
            }
            else
            {
                await _publishEndpoint.Publish(new PaymentFailedEvent { BuyerId = context.Message.BuyerId, orderId = context.Message.OrderId, Message = "not enough balance", orderItems = context.Message.OrderItems });
            }
        }
    }
}
