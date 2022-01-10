using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMongoDbContext _context;
        private readonly IMongoCollection<Models.Product> _collection;


        public ProductController(IMongoDbContext context)
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
