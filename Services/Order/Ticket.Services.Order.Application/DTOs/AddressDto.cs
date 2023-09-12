using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Services.Order.Application.DTOs
{
    public class AddressDto
    {
        public string Country { get;  set; }
        //public string State { get;  set; }
        public string City { get;  set; }
        public string ZipCode { get;  set; }
    }
}
