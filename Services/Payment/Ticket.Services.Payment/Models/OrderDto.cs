namespace Ticket.Services.Payment.Models
{
	public class OrderDto
	{
		public OrderDto()
		{
			OrderItems = new List<OrderItemDto>();
		}
		public string? BuyerId { get; set; }

		public List<OrderItemDto> OrderItems { get; set; }

		public AddressDto? Address { get; set; }
	}

	public class OrderItemDto
	{
		public string? ProductId { get; set; }
		public string? ProductName { get; set; }
		public string? PictureUrl { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
		public decimal Total { get => Price * Quantity; }
	}
	public class AddressDto
	{
		public string? Country { get; set; }
		public string? State { get; set; }
		public string? City { get; set; }
		public string? ZipCode { get; set; }
	}
}
