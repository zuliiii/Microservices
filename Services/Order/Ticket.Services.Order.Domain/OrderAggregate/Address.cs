using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Services.Order.Domain.Core;

namespace Ticket.Services.Order.Domain.OrderAggregate
{
    public class Address : ValueObject
    {
        public Address(string country, string state, string city, string zipCode)
        {
            Country = country;
            State = state;
            City = city;
            ZipCode = zipCode;
        }

        public string Country { get; private set; }
        public string State { get; private set; }
        public string City { get; private set; }
        public string ZipCode { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Country;
            yield return State; 
            yield return City; 
            yield return ZipCode;
        }
    }
}
