using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Ticket.Web.Models.Order
{
	public class CheckoutInfoInput
	{
		[Display(Name = "Country")]
		public string? Country { get; set; }
		[Display(Name = "State")]
		public string? State { get; set; }
		[Display(Name = "City")]
		public string? City { get; set; }
		[Display(Name = "ZipCode")]
		public string? ZipCode { get; set; }

		[Display(Name = "Card Name")]
		public string? CardName { get; set; }
		[Display(Name = "Card Number")]
		public string? CardNumber { get; set; }
		[Display(Name = "Expiration date")]
		public string? Expiration { get; set; }
		[Display(Name = "CVV number")]
		public string? CVV { get; set; }
	}
}
