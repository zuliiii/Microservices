using Microsoft.AspNetCore.Mvc;
using Ticket.Web.Models.Basket;
using Ticket.Web.Services.Interfaces;

namespace Ticket.Web.Controllers
{
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
			var events = await _catalogService.GetByEventId(eventId);

			var basketItem = new BasketItemViewModel { EventId = events.Id, EventTitle = events.Title, Price = events.Price, Quantity=events.Quantity };

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
