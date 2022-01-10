using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Product.API.Models
{
    public class DbContext : IMongoDbContext
    {
        protected readonly IMongoCollection<Product> Collection;
        private readonly MongoDbSettings settings;
        private IMongoDatabase Db { get; set; }
        public DbContext(IOptions<MongoDbSettings> options)
        {
            this.settings = options.Value;
            var client = new MongoClient(this.settings.ConnectionString);
            Db = client.GetDatabase(this.settings.Database);
        }




        public IMongoCollection<Product> GetCollection()
        {
            return Db.GetCollection<Product>(typeof(Product).Name.ToLowerInvariant());
        }
    }
}
