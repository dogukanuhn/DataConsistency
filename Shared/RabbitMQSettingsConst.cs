using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class RabbitMQSettingsConst
    {
        public const string StockReservedEventQueue = "StockReservedEventQueue";
        public const string StockOrderCreatedEventQueue = "StockOrderCreatedEventQueue";
        public const string StockPaymentFailedEventQueue = "StockPaymentFailedEventQueue";
        public const string OrderPaymentCompletedEventQueue = "OrderPaymentCompletedEventQueue";
        public const string OrderPaymentFailedEventQueue = "OrderPaymentFailedEventQueue";
        public const string OrderStockNotReservedEventQueue = "OrderStockNotReservedEventQueue";
    }
}
