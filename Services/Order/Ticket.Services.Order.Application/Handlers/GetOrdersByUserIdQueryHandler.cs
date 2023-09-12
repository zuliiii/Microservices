using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Services.Order.Application.DTOs;
using Ticket.Services.Order.Application.Mapping;
using Ticket.Services.Order.Application.Queries;
using Ticket.Services.Order.Infrastructure;
using Ticket.Shared.DTOs;

namespace Ticket.Services.Order.Application.Handlers
{
    internal class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, Response<List<OrderDto>>>
    {
        private readonly OrderDbContext _context;

        public GetOrdersByUserIdQueryHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Response<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _context.Orders.Include(x => x.OrderItems).Where(x => x.BuyerId == request.UserId).ToListAsync();




            if (!orders.Any())
            {
                return Response<List<OrderDto>>.Success(new List<OrderDto>(), StatusCodes.Status200OK);
            }

            var ordersDto = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);

            return Response<List<OrderDto>>.Success(ordersDto, StatusCodes.Status200OK);
        }
    }
}
