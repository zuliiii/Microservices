using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Ticket.Web.Models;
using Ticket.Web.Models.Order;
using Ticket.Web.Services;
using Ticket.Web.Services.Interfaces;

namespace Ticket.Web.Controllers
{
	public class OrderController : Controller
	{
		private readonly IBasketService _basketService;
		private readonly IOrderService _orderService;
		private readonly IMailService _mailService;
		private readonly IUserService _userService;

		public OrderController(IBasketService basketService, IOrderService orderService, IMailService mailService, IUserService userService)
		{
			_basketService = basketService;
			_orderService = orderService;
			_mailService = mailService;
			_userService = userService;
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
			//1. yol senkron
			var orderStatus = await _orderService.CreateOrder(checkoutInfoInput);
			// 2.yol asenkron 
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

			//2.yol asenkron 
			//return RedirectToAction(""SuccessfulCheckout, new { orderId = new Random().Next(1, 1000) });

		}
		public async Task<IActionResult> SuccessfulCheckout(int orderId)
		{
			var order = await _orderService.GetOrderById(orderId);
			ViewBag.order = order;
			var currentUser = await _userService.GetUser();
			var invoice = MailService.CreateInvoiceTemplate(order);
			_mailService.SendHtmlAsPdfToEmail(currentUser.Email, "Invoice for Ticket", invoice);
			return View();
		}

		public async Task<IActionResult> OrderHistory()
		{
			return View(await _orderService.GetOrder());
		}
	}

}
