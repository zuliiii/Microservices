using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Services.Order.Application.DTOs
{
    public class OrderItemDto
    {
        public string ProductId { get;  set; }
        public string ProductName { get;  set; }
        public string PictureUrl { get;  set; }
        public decimal Price { get;  set; }
        public int Quantity { get;  set; }
        public decimal Total { get => Price * Quantity; }
    }
}
