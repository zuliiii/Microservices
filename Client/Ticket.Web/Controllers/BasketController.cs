using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticket.Web.Models.Basket;
using Ticket.Web.Services.Interfaces;

namespace Ticket.Web.Controllers
{
	[Authorize]
	public class BasketController : Controller
	{
		private readonly ICatalogService _catalogService;
		private readonly IBasketService _basketService;

		public BasketController(ICatalogService catalogService, IBasketService basketService)
		{
			_catalogService = catalogService;
			_basketService = basketService;
		}

		public async Task<IActionResult> Index()
		{
			return View(await _basketService.Get());
		}

		public async Task<IActionResult> AddBasketItem(string eventId)
		{
			var eventt = await _catalogService.GetByEventId(eventId);



			var basketItem = new BasketItemViewModel { EventId=eventt.Id, EventTitle=eventt.Title, Price=eventt.Price, Quantity= eventt.Quantity };

			await _basketService.AddBasketItem(basketItem);

			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> RemoveBasketItem(string eventId)
		{
			var result = await _basketService.RemoveBasketItem(eventId);

			return RedirectToAction(nameof(Index));
		}
	}
}
