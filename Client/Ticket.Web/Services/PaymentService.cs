using System.Diagnostics;
using System.Text.Json;
using Ticket.Web.Models.Basket;
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
			var response = await _httpClient.PostAsJsonAsync<PaymentInfoInput>("http://localhost:5000/services/payment/payments", paymentInfoInput);
			Debug.WriteLine(response.Content.ReadAsStringAsync().Result);

			return response.IsSuccessStatusCode;
		}

		public async Task<string> CreateStripeCheckout(List<BasketItemViewModel> items, int orderId)
		{

			var lineItems = new List<Dictionary<string, string>>();
			var lineItem = new Dictionary<string, string>();
				foreach(var item in items)
			{
				lineItem = CreateLineItem(item.EventTitle, aznToQepik(item.Price), item.Quantity.ToString());
				lineItems.Add(lineItem);
			}

			var requestData = new Dictionary<string, string>
		{
			{ "success_url", "http://localhost:5010/Order/SuccessfulCheckout?orderId="+orderId},
			{ "cancel_url", "https://example.com/cancel" },
			{ "payment_method_types[0]", "card" },
			{ "mode", "payment" }
		};

			for (int i = 0; i < lineItems.Count; i++)
			{
				foreach (var kvp in lineItems[i])
				{
					requestData[$"line_items[{i}]{kvp.Key}"] = kvp.Value;
				}
			}

			var requestContent = new FormUrlEncodedContent(requestData);

			using (HttpClient httpClient = new HttpClient())
			{

				httpClient.DefaultRequestHeaders.Add("Authorization", "Basic c2tfdGVzdF81MU5pMG1xQThmdEZCdVJoVU5YY0p1WEhpMVJhTjVjZ2RUdjRlUUZ6eEZiRVNaUmMxSWZSQWh4NDZDa3AyV1RVaXpEbjNhMUZTbTN3NGl6c1QxUHdnUVlVZDAwNFp0OXBZRjk6");

				var response = await httpClient.PostAsync("https://api.stripe.com/v1/checkout/sessions", requestContent);
				var responseContent = await response.Content.ReadAsStringAsync();
				Debug.WriteLine("Stripe response: " + responseContent);
				var jsonResponse = JsonSerializer.Deserialize<Dictionary<string, object>>(responseContent);

				var url = jsonResponse["url"].ToString();
				return url;
			}
		}

		static Dictionary<string, string> CreateLineItem(string productName, string unitAmount, string quantity)
		{
			return new Dictionary<string, string>
				{
					{ "[price_data][currency]", "azn" },
					{ "[price_data][product_data][name]", productName },
					{ "[price_data][unit_amount]", unitAmount },
					{ "[quantity]","1"  }
				};
		}
		
		static string aznToQepik(decimal price)
		{
			return Convert.ToInt32((price * 100)).ToString();
		}
	}
}
