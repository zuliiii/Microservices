using Ticket.Web.Models;

namespace Ticket.Web.Services.Interfaces
{
	public interface IUserService
	{
		Task<UserViewModel> GetUser();
	}
}
