using MongoDB.Driver;

namespace Product.API
{
    public interface IMongoDbContext
    {
        IMongoCollection<Models.Product> GetCollection();
    }
}
