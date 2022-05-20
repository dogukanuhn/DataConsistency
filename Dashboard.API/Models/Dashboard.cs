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

        public SaleStatus? SaleStatus { get; set; }
        public FailedReason? FailedReason { get; set; }

        public List<Order>? Orders { get; set; }

    }

    public class SaleStatus
    {
        int Success { get; set; }
        int Fail { get; set; }
    }
    public class FailedReason
    {
        int NotEnoughStock { get; set; }
        int NotEnoughBalance { get; set; }
    }
}
