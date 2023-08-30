using Ticket.Web.Models.Basket;
using Ticket.Web.Models.Payment;

namespace Ticket.Web.Services.Interfaces
{
	public interface IPaymentService
	{
		Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput);
		Task<string> CreateStripeCheckout(List<BasketItemViewModel> items, int orderId);

	}
}
