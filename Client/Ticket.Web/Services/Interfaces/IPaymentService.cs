using Ticket.Web.Models.Payment;

namespace Ticket.Web.Services.Interfaces
{
	public interface IPaymentService
	{
		Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput);
	}
}
