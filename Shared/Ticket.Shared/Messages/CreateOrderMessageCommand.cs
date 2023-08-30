using System;
using System.Collections.Generic;
using System.Text;

namespace Ticket.Shared.Messages
{
	public class CreateOrderMessageCommand
	{
        public CreateOrderMessageCommand()
        {
            OrderItems=new List<OrderItem>();
        }
        public string BuyerId { get; set; }
		public List<OrderItem> OrderItems { get; set; }

		public string Country { get; set; }
		public string State { get; set; }
		public string City { get; set; }
		public string ZipCode { get; set; }
	}
	public class OrderItem
	{
        public OrderItem()
        {
			_total = Price * Quantity;
		}
        public string ProductId { get; set; }
		public string ProductName { get; set; }
		public string PictureUrl { get; set; }
		public decimal? Price { get; set; }
		public int Quantity { get; set; }
		private decimal? _total; 

		public decimal? Total
		{
			get => _total; 
			set => _total = value ?? 0; 
		}
		//public decimal? Total => Price * Quantity;
	}
}
