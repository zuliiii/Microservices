using Ticket.Web.Models;
using Ticket.Web.Services.Interfaces;

namespace Ticket.Web.Services
{
	public class UserService : IUserService
	{
		public Task<UserViewModel> GetUser()
		{
			throw new NotImplementedException();
		}
	}
}
