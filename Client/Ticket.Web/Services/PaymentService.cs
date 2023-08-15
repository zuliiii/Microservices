using Ticket.Web.Models.Payment;
using Ticket.Web.Services.Interfaces;

namespace Ticket.Web.Services
{
	public class PaymentService : IPaymentService
	{
		private readonly HttpClient _httpClient;

		public PaymentService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput)
		{
			var response = await _httpClient.PostAsJsonAsync<PaymentInfoInput>("payments", paymentInfoInput);

			return response.IsSuccessStatusCode;
		}
	}
}
