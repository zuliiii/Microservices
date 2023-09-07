namespace Ticket.Web.Models.Order
{
	public class OrderViewModel
	{
		public int Id { get; set; }
		public DateTime? CreatedDate { get; set; }

		//public AddressDto Address { get; set; }

		public string? BuyerId { get; set; }

		public List<OrderItemViewModel>? OrderItems { get; set; }

		public decimal? TotalPrice => OrderItems.Sum(x => x.Price*x.Quantity);
	}
}
