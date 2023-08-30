using MassTransit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Services.Order.Infrastructure;
using Ticket.Shared.Messages;

namespace Ticket.Services.Order.Application.Consumers
{
	public class EventNameChangedEventConsumer : IConsumer<EventNameChangedEvent>
	{
		private readonly OrderDbContext _orderDbContext;

		public EventNameChangedEventConsumer(OrderDbContext orderDbContext)
		{
			_orderDbContext = orderDbContext;
		}

		public async Task Consume(ConsumeContext<EventNameChangedEvent> context)
		{
			var orderItems = await _orderDbContext.OrderItems.Where(x => x.ProductId == context.Message.EventId).ToListAsync();


			orderItems.ForEach(x =>
			{
				x.UpdateOrderItem(context.Message.UpdatedName, x.PictureUrl, x.Price, x.Quantity);
			});

			await _orderDbContext.SaveChangesAsync();

		}
	}
}
