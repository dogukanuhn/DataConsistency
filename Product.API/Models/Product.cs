using MongoDB.Bson;

namespace Product.API.Models
{
    public class Product
    {
        public ObjectId Id { get; set; }
        public string ProductId { get; set; }
        public string Image { get; set; }
        public string Name  { get; set; }
        public string IngredientsText  { get; set; }
        public double BasePrice { get; set; }
        public double SellPrice { get; set; }
    }
}
