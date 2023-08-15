namespace Ticket.Services.Bakset.DTOs
{
    public class BasketDto
    {
        public string? UserId { get; set; }
        public string? DiscountCode { get; set; }
		public int? DiscountRate { get; set; }

		public List<BasketItemDto> basketItems { get; set; }

        public decimal TotalPrice 
        {
            get => basketItems.Sum(x => x.Quantity * x.Price);
        }
    }
}
