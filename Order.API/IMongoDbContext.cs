using MongoDB.Driver;

namespace Order.API
{
    public interface IMongoDbContext
    {
        IMongoCollection<Models.Order> GetCollection();
    }
}
