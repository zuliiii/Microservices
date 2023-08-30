using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Services.Order.Infrastructure;
using Ticket.Shared.Messages;

namespace Ticket.Services.Order.Application.Consumers
{
	public class CreateOrderMessageCommandConsumer: IConsumer<CreateOrderMessageCommand>
	{
		private readonly OrderDbContext _orderDbContext;

		public CreateOrderMessageCommandConsumer(OrderDbContext orderDbContext)
		{
			_orderDbContext = orderDbContext;
		}

		public async Task Consume(ConsumeContext<CreateOrderMessageCommand> context)
		{
			var newAddress = new Domain.OrderAggregate.Address(context.Message.State, context.Message.Country, context.Message.City, context.Message.ZipCode);

			Domain.OrderAggregate.Order order = new Domain.OrderAggregate.Order(context.Message.BuyerId, newAddress);

			context.Message.OrderItems.ForEach(x =>
			{
				order.AddOrderItem(x.ProductId, x.ProductName, x.Price, x.PictureUrl, x.Quantity);
			});

			await _orderDbContext.Orders.AddAsync(order);

			await _orderDbContext.SaveChangesAsync();
		}
	}
}
