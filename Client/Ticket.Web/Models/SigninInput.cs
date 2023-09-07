﻿using System.ComponentModel.DataAnnotations;

namespace Ticket.Web.Models
{
    public class SigninInput
    {
		[Required]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }

		
	}
}
