using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Order.API.Models
{
    public class DbContext : IMongoDbContext
    {
        protected readonly IMongoCollection<Order> Collection;
        private readonly MongoDbSettings settings;
        private IMongoDatabase Db { get; set; }
        public DbContext(IOptions<MongoDbSettings> options)
        {
            this.settings = options.Value;
            var client = new MongoClient(this.settings.ConnectionString);
            Db = client.GetDatabase(this.settings.Database);
        }




        public IMongoCollection<Order> GetCollection()
        {
            return Db.GetCollection<Order>(typeof(Order).Name.ToLowerInvariant());
        }
    }
}
