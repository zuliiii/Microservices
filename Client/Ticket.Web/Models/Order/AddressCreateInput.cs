namespace Ticket.Web.Models.Order
{
	public class AddressCreateInput
	{
		public string? Country { get; set; }
		public string? State { get; set; }
		public string? City { get; set; }
		public string? ZipCode { get; set; }
	}
}
