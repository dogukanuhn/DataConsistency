using Dashboard.API.Models;
using Dashboard.API.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

            DashboardService service = new DashboardService(JsonSerializer.Deserialize<List<Order>>(responseBody));

            return Ok(JsonSerializer.Deserialize<List<Order>>(responseBody));
        }


    }
}
