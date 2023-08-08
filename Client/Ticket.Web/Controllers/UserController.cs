using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticket.Web.Services.Interfaces;

namespace Ticket.Web.Controllers
{
	[Authorize]
	public class UserController : Controller
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		public async Task<IActionResult> Index()
		{
			return View(await _userService.GetUser());
		}
	}
}
