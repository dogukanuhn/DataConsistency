using MongoDB.Bson;

namespace Stock.API.Models
{
    public class Stock
    {
        public ObjectId Id { get; set; }
        public string ProductId { get; set; }
        public int Count { get; set; }
    }
}
