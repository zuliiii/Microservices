namespace Ticket.Services.Bakset.DTOs
{
    public class BasketItemDto
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string EventId { get; set;}
        public string EventTitle { get; set;}
    }
}
