using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Ticket.Web.Exceptions;
using Ticket.Web.Models;
using Ticket.Web.Services.Interfaces;

namespace Ticket.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICatalogService _catalogService;

        public HomeController(ILogger<HomeController> logger, ICatalogService catalogService)
        {
            _logger = logger;
            _catalogService = catalogService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _catalogService.GetAllEventAsync());
        }

        public async Task<IActionResult> Detail(string id)
        {
            var item = await _catalogService.GetByEventId(id);

            return View(item);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
			var errorFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();

			if (errorFeature != null && errorFeature.Error is UnAuthorizeException)
			{
				return RedirectToAction(nameof(AuthController.Logout), "Auth");
			}

			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}