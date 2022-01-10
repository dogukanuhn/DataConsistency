namespace Order.API.DTOs
{
  
        public class OrderCreateDto
        {
            public List<OrderItemDto> orderItems { get; set; }
            public PaymentDto payment { get; set; }
            public AddressDto Address { get; set; }
        }

        public class OrderItemDto
        {
            public string ProductId { get; set; }
            public int Count { get; set; }
            public decimal Price { get; set; }
        }

        public class PaymentDto
        {
            public string CardName { get; set; }
            public string CardNumber { get; set; }
            public string ExpireYear { get; set; }
            public string ExpireMonth { get; set; }
        public string CVV { get; set; }
        }

        public class AddressDto
        {
            public string Line { get; set; }
            public string Province { get; set; }
            public string District { get; set; }
        }
    }

