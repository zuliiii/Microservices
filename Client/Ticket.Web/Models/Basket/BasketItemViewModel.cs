namespace Ticket.Web.Models.Basket
{
	public class BasketItemViewModel
	{
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public string EventId { get; set; }
		public string EventTitle { get; set; }

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
