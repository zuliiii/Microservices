using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticket.Shared.Services;
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
	}
}
