using Dashboard.API.Models;
using Dashboard.API.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;

namespace Dashboard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync("https://localhost:5001/api/order");
            string responseBody = await response.Content.ReadAsStringAsync();
            
            List<Order> ConvertedOrders = JsonConvert.DeserializeObject<List<Order>>(responseBody);

            DashboardService service = new DashboardService(ConvertedOrders);

            return Ok(service.Dashboard);
        }


    }
}
