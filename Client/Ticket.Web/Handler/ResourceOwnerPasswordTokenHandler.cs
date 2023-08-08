using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Ticket.Web.Exceptions;
using Ticket.Web.Services.Interfaces;

namespace Ticket.Web.Handler
{
	public class ResourceOwnerPasswordTokenHandler:DelegatingHandler
	{
		private readonly IHttpContextAccessor _contextAccessor;
		private readonly IIdentityService _identityService;
		private readonly ILogger<ResourceOwnerPasswordTokenHandler> _logger;

		public ResourceOwnerPasswordTokenHandler(IHttpContextAccessor contextAccessor, IIdentityService identityService, ILogger<ResourceOwnerPasswordTokenHandler> logger)
		{
			_contextAccessor = contextAccessor;
			_identityService = identityService;
			_logger = logger;
		}

		protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var accessToken = await _contextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

			request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

			var response = await base.SendAsync(request, cancellationToken);

			if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
			{
				var tokenResponse = await _identityService.GetAccessTokenByRefreshToken();

				if (tokenResponse != null)
				{
					request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);

					response = await base.SendAsync(request, cancellationToken);
				}
			}

			if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
			{
				throw new UnAuthorizeException();
			}

			return response;
		}

		//protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		//{
		//	var accessToken = await _contextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

		//	request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

		//	var response = await base.SendAsync(request, cancellationToken);

		//	if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
		//	{
		//		var tokenResponse = _identityService.GetAccessTokenByRefreshToken();
		//		if(tokenResponse != null)
		//		{
		//			request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
		//				"Bearer", tokenResponse.AccessToken);

		//			var response = await base.SendAsync(request, cancellationToken);

		//		}
		//	}
		//}

	}
}
