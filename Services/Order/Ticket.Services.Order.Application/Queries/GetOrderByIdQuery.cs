using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Services.Order.Application.DTOs;
using Ticket.Shared.DTOs;

namespace Ticket.Services.Order.Application.Queries
{
    public class GetOrderByIdQuery : IRequest<Response<OrderDto>>
    {
        public int OrderId { get; set; }
    }
}
