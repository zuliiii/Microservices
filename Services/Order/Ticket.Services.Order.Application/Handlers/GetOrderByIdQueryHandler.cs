using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Services.Order.Application.DTOs;
using Ticket.Services.Order.Application.Queries;
using Ticket.Services.Order.Application.Mapping;
using Ticket.Services.Order.Infrastructure;
using Ticket.Shared.DTOs;

namespace Ticket.Services.Order.Application.Handlers
{
	internal class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Response<OrderDto>>
	{
		private readonly OrderDbContext _context;

		public GetOrderByIdQueryHandler(OrderDbContext context)
		{
			_context = context;
		}

		public async Task<Response<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
		{
			var order = await _context.Orders.Include(x => x.OrderItems).Where(x => x.Id == request.OrderId).FirstAsync<Domain.OrderAggregate.Order>();

			if (order != null)
			{
				return Response<OrderDto>.Fail("Order is not founded", 404);
			}

			var orderDto = ObjectMapper.Mapper.Map<OrderDto>(order);

			return Response<OrderDto>.Success(orderDto, StatusCodes.Status200OK);
		}
	}
}