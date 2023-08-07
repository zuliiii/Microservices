using Microsoft.AspNetCore.Mvc;
using Ticket.Web.Models;

namespace Ticket.Web.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult SignIn()
        {
            return View();
        }


        [HttpPost]
		public async Task<IActionResult> SignIn(SigninInput signinInput)
		{
			return View();
		}
	}
}
