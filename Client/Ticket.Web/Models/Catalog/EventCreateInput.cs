﻿using System.ComponentModel.DataAnnotations;

namespace Ticket.Web.Models.Catalog
{
	public class EventCreateInput
	{
		public string Title { get; set; } 
		public string Description { get; set; } 
		public string? UserId { get; set; }
		public decimal Price { get; set; }
		public string? Picture { get; set; } 
		public string? Location { get; set; } 
		public string? CategoryId { get; set; }

		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yyyy-MM-dd}")]
		public DateTime EventDateTime { get; set; }


		public IFormFile? PhotoFormFile { get; set; }
	}
}
