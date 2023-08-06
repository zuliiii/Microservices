using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Ticket.Web.Models
{
    public class SigninInput
    {
        [Required]
        [Display(Name = "Enter your email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Enter your password")]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool IsRemember { get; set; }
    }
}
