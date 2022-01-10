using MongoDB.Driver;

namespace Stock.API
{
    public interface IMongoDbContext
    {
        IMongoCollection<Models.Stock> GetCollection();
    }
}
