using Dashboard.API.Models;
using System.Linq;

namespace Dashboard.API.services
{
    public class DashboardService
    {
        public List<Order> orders { get; }
        public DashboardService(List<Order> orders)
        {
            this.orders = orders;
            CalculateFailedReason();
        }

        //public Models.Dashboard Get()
        //{
        //    FailedReason reason = CalculateFailedReason();

        //    return new Models.Dashboard();
        //}

        private FailedReason CalculateFailedReason()
        {
            var list = orders.GroupBy(x => x.FailMessage).Select(grp => new {name=grp.Key,count= grp.Count()}).ToList();


            return new FailedReason
            {

            };
        }

    }
}
