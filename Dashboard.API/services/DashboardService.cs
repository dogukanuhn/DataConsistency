using Dashboard.API.Models;

namespace Dashboard.API.services
{
    public class DashboardService
    {
        public List<Order> orders { get; }
        public DashboardService(List<Order> orders)
        {
            this.orders = orders;
        }

        //public Models.Dashboard Get()
        //{
            

        //    return new Models.Dashboard();
        //}

    }
}
