using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Net.Http;
using Ticket.Shared.DTOs;
using Ticket.Shared.Services;
using Ticket.Web.Models.Order;
using Ticket.Web.Models.Payment;
using Ticket.Web.Services.Interfaces;

namespace Ticket.Web.Services
{
	public class OrderService : IOrderService
	{
		private readonly IPaymentService _paymentService;
		private readonly HttpClient _httpClient;
		private readonly IBasketService _basketService;
		private readonly ISharedIdentityService _sharedIdentityService;

		public OrderService(IPaymentService paymentService, HttpClient httpClient, IBasketService basketService, ISharedIdentityService sharedIdentityService)
		{
			_paymentService = paymentService;
			_httpClient = httpClient;
			_basketService = basketService;
			_sharedIdentityService = sharedIdentityService;
		}

		public async Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput)
		{
			var basket = await _basketService.Get();

			var orderCreateInput = new OrderCreateInput()
			{
				BuyerId = _sharedIdentityService.GetUserId,
				Address = new AddressCreateInput { Country = checkoutInfoInput.Country, State = checkoutInfoInput.State, City = checkoutInfoInput.City, ZipCode = checkoutInfoInput.ZipCode },
			};

			var paymentInfoInput = new PaymentInfoInput()
			{
				CardName = checkoutInfoInput.CardName,
				CardNumber = checkoutInfoInput.CardNumber,
				Expiration = checkoutInfoInput.Expiration,
				CVV = checkoutInfoInput.CVV,
				TotalPrice = basket.TotalPrice,
				Order = orderCreateInput
			};
			var responsePayment = await _paymentService.ReceivePayment(paymentInfoInput);

			if (!responsePayment)
			{
				return new OrderCreatedViewModel() { Error = "Ödeme alınamadı", IsSuccessful = false };
			}


			basket.BasketItems.ForEach(x =>
			{
				var orderItem = new OrderItemCreateInput { ProductId = x.EventId, Price = x.GetCurrentPrice, PictureUrl = "", ProductName = x.EventTitle, Quantity = x.Quantity };
				orderCreateInput.OrderItems.Add(orderItem);
			});


				var response = await _httpClient.PostAsJsonAsync<OrderCreateInput>("orders", orderCreateInput);
            if (!response.IsSuccessStatusCode)
			{
				return new OrderCreatedViewModel() { Error = "Sipariş oluşturulamadı", IsSuccessful = false };
			}

			var orderCreatedViewModel = await response.Content.ReadFromJsonAsync<Response<OrderCreatedViewModel>>();

			var checkoutUrl = await _paymentService.CreateStripeCheckout(basket.BasketItems, orderCreatedViewModel.Data.OrderId);
			orderCreatedViewModel.Data.IsSuccessful = true;
			orderCreatedViewModel.Data.checkoutUrl = checkoutUrl;

			return orderCreatedViewModel.Data;
        }

		public async Task<List<OrderViewModel>> GetOrder()
		{
			var response = await _httpClient.GetFromJsonAsync<Response<List<OrderViewModel>>>("orders");

			return response.Data;
		}

		public async Task<OrderViewModel> GetOrderById(int orderId)
		{
			var response = await _httpClient.GetAsync($"orders/GetOrderById?orderId={orderId}");
			var responseSuccess = await response.Content.ReadFromJsonAsync<Response<OrderViewModel>>();

			return responseSuccess.Data;
		}

		//public async Task<OrderViewModel> GetOrderById(int orderId)
		//{
		//	throw new NotImplementedException();
		//	//var response = await _httpClient.GetFromJsonAsync<Response<List<OrderViewModel>>>("orders");

		//	//return response;
		//}

		public async Task<OrderSuspendViewModel> SuspendOrder(CheckoutInfoInput checkoutInfoInput)
		{
			var basket = await _basketService.Get();

			var orderCreateInput = new OrderCreateInput()
			{
				BuyerId = _sharedIdentityService.GetUserId,
				Address = new AddressCreateInput { Country = checkoutInfoInput.Country, State = checkoutInfoInput.State, City = checkoutInfoInput.City, ZipCode = checkoutInfoInput.ZipCode },
			};

			basket.BasketItems.ForEach(x =>
			{
				var orderItem = new OrderItemCreateInput { ProductId = x.EventId, Price = x.GetCurrentPrice, PictureUrl = "", ProductName = x.EventTitle, Quantity = x.Quantity };
				orderCreateInput.OrderItems.Add(orderItem);
			});

			var paymentInfoInput = new PaymentInfoInput()
			{
				CardName = checkoutInfoInput.CardName,
				CardNumber = checkoutInfoInput.CardNumber,
				Expiration = checkoutInfoInput.Expiration,
				CVV = checkoutInfoInput.CVV,
				TotalPrice = basket.TotalPrice,
				Order= orderCreateInput,
			};
			var responsePayment = await _paymentService.ReceivePayment(paymentInfoInput);

			if (!responsePayment)
			{
				return new OrderSuspendViewModel() { Error = "Ödeme alınamadı", IsSuccessful = false };
			}
			await _basketService.Delete();
			return new OrderSuspendViewModel() { IsSuccessful = true };
		}
	}
}
