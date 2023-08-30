using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Ticket.Web.Models.Order;
using Ticket.Web.Services;
using Ticket.Web.Services.Interfaces;

namespace Ticket.Web.Controllers
{
	public class OrderController : Controller
	{
		private readonly IBasketService _basketService;
		private readonly IOrderService _orderService;

		public OrderController(IBasketService basketService, IOrderService orderService)
		{
			_basketService = basketService;
			_orderService = orderService;
		}

		public async Task<IActionResult> Checkout()
		{
			var basket = await _basketService.Get();
			ViewBag.basket = basket;
			return View(new CheckoutInfoInput());
		}

		[HttpPost]
		public async Task<IActionResult> Checkout(CheckoutInfoInput checkoutInfoInput)
		{
			//1. yol senkron iletişim
			var orderStatus = await _orderService.CreateOrder(checkoutInfoInput);
			// 2.yol asenkron iletişim
			//var orderStatus = await _orderService.SuspendOrder(checkoutInfoInput);

			if (!orderStatus.IsSuccessful)
			{
				var basket = await _basketService.Get();
				ViewBag.basket = basket;
				ViewBag.error = orderStatus.Error;

				return View();
			}
			//1. yol senkron iletişim
			return Redirect(orderStatus.checkoutUrl);

			//2.yol asenkron iletişim
			//return RedirectToAction(""SuccessfulCheckout, new { orderId = new Random().Next(1, 1000) });

		}
		public async Task<IActionResult> SuccessfulCheckout(int orderId)
		{
			var order = await _orderService.GetOrderById(orderId);
			ViewBag.order = order;
			return View();
		}

		public async Task<IActionResult> OrderHistory()
		{
			return View(await _orderService.GetOrder());
		}
	}

}
