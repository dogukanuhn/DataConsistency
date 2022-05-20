using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Dashboard.API.Models
{
    public class Order
    {
        public string OrderId { get; set; }
        public DateTime CreatedDate { get; set; }

        public Address Address { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

        public OrderStatus Status { get; set; }

        public string FailMessage { get; set; }
    }

    public enum OrderStatus
    {
        Suspend,
        Complete,
        Fail
    }
}
