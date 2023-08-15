namespace Ticket.Web.Models.Order
{
	public class OrderCreateInput
	{
        public OrderCreateInput()
        {
			OrderItems = new List<OrderItemCreateInput>();
		}
        public string BuyerId { get; set; }

		public List<OrderItemCreateInput> OrderItems { get; set; }

		public AddressCreateInput Address { get; set; }
	}
}
