using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Stock.API.Models
{
    public class DbContext : IMongoDbContext
    {
        protected readonly IMongoCollection<Stock> Collection;
        private readonly MongoDbSettings settings;
        private IMongoDatabase Db { get; set; }
        public DbContext(IOptions<MongoDbSettings> options)
        {
            this.settings = options.Value;
            var client = new MongoClient(this.settings.ConnectionString);
            Db = client.GetDatabase(this.settings.Database);
        }




        public IMongoCollection<Stock> GetCollection()
        {
            return Db.GetCollection<Stock>(typeof(Stock).Name.ToLowerInvariant());
        }
    }
}
