using Dashboard.API.helper;
using Dashboard.API.Models;

namespace Dashboard.API.services
{
    public class DashboardService
    {
        public List<Order> orders { get; }
        public Models.Dashboard Dashboard { get; set; }
        public DashboardService(List<Order> orders)
        {
            this.orders = orders;

            Dashboard = new Models.Dashboard(CalculateFailedReason(), orders, CalculateSaleStatus());
        }


        public FailedReason CalculateFailedReason()
        {

            var list = orders.GroupBy(x => x.FailMessage)
                .Select(grp => new { name = grp.Key?.ToString().ToTitleCase().Replace(" ", ""), count = grp.Count() })
                .ToList();


            return new FailedReason
            {
                NotEnoughBalance = list.Where(x => x.name == "NotEnoughBalance").ToList()[0].count,
                NotEnoughStock = list.Where(x => x.name == "NotEnoughStock").ToList()[0].count
            };
        }

        private SaleStatus CalculateSaleStatus()
        {

            var list = orders.GroupBy(x => x.Status)
                .Select(grp => new { name = grp.Key.ToString() == "Fail" ? "Suspend" : "Success", count = grp.Count() })
                .ToList();


            return new SaleStatus
            {
                Suspend = list.Where(x => x.name == "Suspend").ToList()[0].count,
                Success = list.Where(x => x.name == "Success").ToList()[0].count
            };
        }
    }

}
