using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Stock.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {

        private readonly IMongoDbContext _context;
        private readonly IMongoCollection<Models.Stock> _collection;

        public StockController(IMongoDbContext context)
        {
            _context = context;
            _collection = _context.GetCollection();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok((await _collection.FindAsync(_ => true)).ToListAsync());
        }
    }
}
