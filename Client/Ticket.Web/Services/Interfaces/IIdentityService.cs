using IdentityModel.Client;
using Ticket.Shared.DTOs;
using Ticket.Web.Models;

namespace Ticket.Web.Services.Interfaces
{
    public interface IIdentityService
    {
		Task<Response<bool>> SignIn(SigninInput signinInput);

		Task<TokenResponse> GetAccessTokenByRefreshToken();

		Task RevokeRefreshToken();

	}
}
