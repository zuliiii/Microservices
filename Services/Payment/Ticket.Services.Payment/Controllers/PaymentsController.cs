using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticket.Services.Payment.Models;
using Ticket.Shared.ControllerBases;
using Ticket.Shared.DTOs;
using Ticket.Shared.Messages;

namespace Ticket.Services.Payment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : CustomBaseController
    {
		private readonly ISendEndpointProvider _sendEndpointProvider;

		public PaymentsController(ISendEndpointProvider sendEndpointProvider)
		{
			_sendEndpointProvider = sendEndpointProvider;
		}

		[HttpPost]
        public async Task<IActionResult> ReceivePayment(PaymentDto paymentDto)
        {
			var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-order-service"));

			var createOrderMessageCommand = new CreateOrderMessageCommand();// state yoxdumendeeeeeeeeeeeeeeeeeee
			createOrderMessageCommand.BuyerId = paymentDto.Order.BuyerId;
			//createOrderMessageCommand.State = paymentDto.Order.Address.State;
			createOrderMessageCommand.Country = paymentDto.Order.Address.Country;
			createOrderMessageCommand.City= paymentDto.Order.Address.City;
			createOrderMessageCommand.ZipCode= paymentDto.Order.Address.ZipCode;

			paymentDto.Order.OrderItems.ForEach(x =>
			{
				createOrderMessageCommand.OrderItems.Add(new OrderItem
				{
					PictureUrl = x.PictureUrl,
					Price = x.Price,
					ProductId = x.ProductId,
					ProductName = x.ProductName,
					Quantity = x.Quantity,
					Total = x.Total
				});
			});

			await sendEndpoint.Send<CreateOrderMessageCommand>(createOrderMessageCommand);

			return CreateActionResultInstance(Shared.DTOs.Response<NoContent>.Success(StatusCodes.Status200OK));
        }
    }
}
