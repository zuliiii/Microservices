using System.ComponentModel.DataAnnotations;

namespace Ticket.Web.Models
{
	public class SignUpInput
	{
		[Required]
		public string UserName { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
		[Required]
		public string City { get; set; }
	}
}
