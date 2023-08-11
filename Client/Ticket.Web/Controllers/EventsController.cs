using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ticket.Shared.Services;
using Ticket.Web.Models.Catalog;
using Ticket.Web.Services.Interfaces;

namespace Ticket.Web.Controllers
{
	[Authorize]
	public class EventsController : Controller
	{
		private readonly ICatalogService _catalogService;
		private readonly ISharedIdentityService _sharedIdentityService;

		public EventsController(ICatalogService catalogService, ISharedIdentityService sharedIdentityService)
		{
			_catalogService = catalogService;
			_sharedIdentityService = sharedIdentityService;
		}

		public async Task<IActionResult> Index()
		{
			return View(await _catalogService.GetAllEventByUserIdAsync(_sharedIdentityService.GetUserId));
		}

		public async Task<IActionResult> Create()
		{
			var categories = await _catalogService.GetAllCategoryAsync();

			ViewBag.categoryList = new SelectList(categories, "Id", "Name");

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(EventCreateInput eventCreateInput)
		{
			var categories = await _catalogService.GetAllCategoryAsync();

			ViewBag.categoryList = new SelectList(categories, "Id", "Name");

			if (!ModelState.IsValid)
			{
				return View();
			}
			eventCreateInput.UserId = _sharedIdentityService.GetUserId;

			await _catalogService.CreateEventAsync(eventCreateInput);

		
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Update(string id)
		{
			var events = await _catalogService.GetByEventId(id);
			var categories = await _catalogService.GetAllCategoryAsync();

			if (events == null)
			{
				//mesaj göster
				RedirectToAction(nameof(Index));
			}
			ViewBag.categoryList = new SelectList(categories, "Id", "Name", events.Id);

			EventUpdateInput eventUpdateInput = new()
			{
				Id=events.Id,
				Title = events.Title,
				Description = events.Description,
				UserId = events.UserId,
				Price = events.Price,
				Picture = events.Picture,
				Location = events.Location,
				CategoryId	= events.CategoryId
			};
			return View(eventUpdateInput);
		}

		[HttpPost]
		public async Task<IActionResult> Update(EventUpdateInput eventUpdateInput)
		{
			var categories = await _catalogService.GetAllCategoryAsync();
			ViewBag.categoryList = new SelectList(categories, "Id", "Name", eventUpdateInput.Id);
			if (!ModelState.IsValid)
			{
				return View();
			}
			await _catalogService.UpdateEventAsync(eventUpdateInput);

			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Delete(string id)
		{
			await _catalogService.DeleteEventAsync(id);

			return RedirectToAction(nameof(Index));
		}
	}
}
