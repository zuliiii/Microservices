namespace Ticket.Web.Models.Order
{
	public interface OrderViewModel
	{
		public int Id { get; set; }
		public DateTime CreatedDate { get; set; }

		//public AddressDto Address { get; set; }

		public string BuyerId { get; set; }

		public List<OrderItemViewModel> OrderItems { get; set; }
	}
}
