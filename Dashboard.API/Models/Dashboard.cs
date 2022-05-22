using Dashboard.API.Models;

namespace Dashboard.API.Models
{
    public class Dashboard
    {

        public Dashboard(FailedReason? failedReason, List<Order>? orders, SaleStatus? saleStatus)
        {
            FailedReason = failedReason;
            Orders = orders;
            SaleStatus = saleStatus;
        }

        public SaleStatus SaleStatus { get; set; }
        public FailedReason FailedReason { get; set; }

        public List<Order> Orders { get; set; }

    }

    public class SaleStatus
    {
        public int Success { get; set; }
        public int Suspend { get; set; }
    }
    public class FailedReason
    {
        public int NotEnoughStock { get; set; }
        public int NotEnoughBalance { get; set; }
    }
}
