namespace Ticket.Web.Models.Order
{
	public class SuccessfulCheckoutView
	{
		public int OrderId { get; set; }

		public string? UserId { get; set; }
		public string Title { get; set; }
		public decimal Price { get; set; }
		public string? Location { get; set; }
		public int Quantity { get; set; }
		public decimal ItemTotalPrice => Quantity * Price;
		public string? Picture { get; set; }
	}
}
