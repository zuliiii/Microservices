using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Ticket.Web.Models;
using Ticket.Web.Services.Interfaces;

namespace Ticket.Web.Controllers
{

	public class AuthController : Controller
	{
		private readonly IIdentityService _identityService;

		public AuthController(IIdentityService identityService)
		{
			_identityService = identityService;
		}

		public IActionResult SignIn()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignIn(SigninInput signinInput)
		{
			if (!ModelState.IsValid)
			{
				return View(signinInput);
			}

			var response = await _identityService.SignIn(signinInput);

			if (!response.isSuccessful)
			{
				response.Errors.ForEach(x =>
				{
					ModelState.AddModelError(String.Empty, x);
				});

				return View();
			}

			return RedirectToAction(nameof(Index), "Home");
		}
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			await _identityService.RevokeRefreshToken();
			return RedirectToAction(nameof(HomeController.Index), "Home");
		}

	}

}
