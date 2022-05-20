namespace Dashboard.API.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string ProductId { get; set; }

        public decimal Price { get; set; }

        public string OrderId { get; set; }

        public Order Order { get; set; }

        public int Count { get; set; }
    }
}
