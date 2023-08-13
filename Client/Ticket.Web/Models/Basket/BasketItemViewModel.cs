namespace Ticket.Web.Models.Basket
{
	public class BasketItemViewModel
	{
       
        public string EventId { get; set; }
		public string EventTitle { get; set; }
		public int Quantity { get; set; } = 2;
		public decimal Price { get; set; }
	
		private decimal? DiscountAppliedPrice;

		public decimal GetCurrentPrice
		{
			get => DiscountAppliedPrice != null ? DiscountAppliedPrice.Value : Price;
		}

		public void AppliedDiscount(decimal discountPrice)
		{
			DiscountAppliedPrice = discountPrice;
		}
	}
}
