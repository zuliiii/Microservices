using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Security.Claims;
using System.Text.Json;
using Ticket.Shared.DTOs;
using Ticket.Web.Models;
using Ticket.Web.Services.Interfaces;


namespace Ticket.Web.Services
{
	public class IdentityService : IIdentityService
	{
		private readonly HttpClient _httpClient;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly ClientSettings _clientSettings;
		private readonly ServiceApiSettings _serviceApiSettings;

		public IdentityService(HttpClient client, IHttpContextAccessor httpContextAccessor, IOptions<ClientSettings> clientSettings, IOptions<ServiceApiSettings> serviceApiSettings)
		{
			_httpClient = client;
			_httpContextAccessor = httpContextAccessor;
			_clientSettings = clientSettings.Value;
			_serviceApiSettings = serviceApiSettings.Value;
		}

		public async Task<TokenResponse> GetAccessTokenByRefreshToken()
		{
			var discovery = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
			{
				Address = _serviceApiSettings.IdentityBaseUri,
				Policy = new DiscoveryPolicy { RequireHttps = false }
			});

			if (discovery.IsError)
			{
				throw discovery.Exception;
			}

			var refreshToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

			RefreshTokenRequest refreshTokenRequest = new()
			{
				ClientId = _clientSettings.WebClientForUser.ClientId,
				ClientSecret = _clientSettings.WebClientForUser.ClientSecret,
				RefreshToken = refreshToken,
				Address = discovery.TokenEndpoint
			};

			var token = await _httpClient.RequestRefreshTokenAsync(refreshTokenRequest);

			if (token.IsError)
			{
				return null;
			}

			var authenticationTokens = new List<AuthenticationToken>()
			{
				new AuthenticationToken{ Name=OpenIdConnectParameterNames.AccessToken,Value=token.AccessToken},
				   new AuthenticationToken{ Name=OpenIdConnectParameterNames.RefreshToken,Value=token.RefreshToken},

					  new AuthenticationToken{ Name=OpenIdConnectParameterNames.ExpiresIn,Value= DateTime.Now.AddSeconds(token.ExpiresIn).ToString("o",CultureInfo.InvariantCulture)}
			};

			var authenticationResult = await _httpContextAccessor.HttpContext.AuthenticateAsync();

			var properties = authenticationResult.Properties;
			properties.StoreTokens(authenticationTokens);

			await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, authenticationResult.Principal, properties);

			return token;
		}

		public async Task RevokeRefreshToken()
		{
			var discovery = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
			{
				Address = _serviceApiSettings.IdentityBaseUri,
				Policy = new DiscoveryPolicy { RequireHttps = false }
			});

			if (discovery.IsError)
			{
				throw discovery.Exception;
			}
			var refreshToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

			TokenRevocationRequest tokenRevocationRequest = new()
			{
				ClientId = _clientSettings.WebClientForUser.ClientId,
				ClientSecret = _clientSettings.WebClientForUser.ClientSecret,
				Address = discovery.RevocationEndpoint,
				Token = refreshToken,
				TokenTypeHint = "refresh_token"
			};

			await _httpClient.RevokeTokenAsync(tokenRevocationRequest);
		}

		public async Task<Response<bool>> SignIn(SigninInput signinInput)
		{
			var discovery = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
			{
				Address = _serviceApiSettings.IdentityBaseUri,
				Policy = new DiscoveryPolicy { RequireHttps = false }
			});

			if (discovery.IsError)
			{
				throw discovery.Exception;
			}

			var passwordTokenRequest = new PasswordTokenRequest
			{
				ClientId = _clientSettings.WebClientForUser.ClientId,
				ClientSecret = _clientSettings.WebClientForUser.ClientSecret,
				UserName = signinInput.Email,
				Password = signinInput.Password,
				Address = discovery.TokenEndpoint
			};

			var token = await _httpClient.RequestPasswordTokenAsync(passwordTokenRequest);

			if (token.IsError)
			{
				var responseContent = await token.HttpResponse.Content.ReadAsStringAsync();

				var errorDto = JsonSerializer.Deserialize<ErrorDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

				return Response<bool>.Fail(errorDto.Errors, StatusCodes.Status400BadRequest);
			}

			var userInfoRequest = new UserInfoRequest
			{
				Token = token.AccessToken,
				Address = discovery.UserInfoEndpoint
			};

			var userInfo = await _httpClient.GetUserInfoAsync(userInfoRequest);

			if (userInfo.IsError)
			{
				throw userInfo.Exception;
			}

			ClaimsIdentity claimsIdentity = new ClaimsIdentity(userInfo.Claims, CookieAuthenticationDefaults.AuthenticationScheme, "name", "role");

			ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

			var authenticationProperties = new AuthenticationProperties();

			authenticationProperties.StoreTokens(new List<AuthenticationToken>()
			{
				new AuthenticationToken{ Name=OpenIdConnectParameterNames.AccessToken,Value=token.AccessToken},
				new AuthenticationToken{ Name=OpenIdConnectParameterNames.RefreshToken,Value=token.RefreshToken},
				new AuthenticationToken{ Name=OpenIdConnectParameterNames.ExpiresIn,Value= DateTime.Now.AddSeconds(token.ExpiresIn).ToString("o",CultureInfo.InvariantCulture)}
			});

			
			await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties);

			return Response<bool>.Success(StatusCodes.Status200OK);
		}

		public async Task<Response<bool>> SignUp(SignUpInput signUpInput)
		{
			var discovery = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
			{
				Address = _serviceApiSettings.IdentityBaseUri,
				Policy = new DiscoveryPolicy { RequireHttps = false }
			});

			if (discovery.IsError)
			{
				throw discovery.Exception;
			}

			var userCreationResponse = await _httpClient.PostAsJsonAsync("http://localhost:5001/api/User/SignUp", signUpInput);

			Debug.WriteLine("SignUp: " + userCreationResponse.Content.ReadAsStringAsync());
			Debug.WriteLine("SignUp: " + userCreationResponse.Content.ReadAsStringAsync().Result);

			if (!userCreationResponse.IsSuccessStatusCode)
			{
				// Handle signup failure, e.g., display error messages
				var responseContent = await userCreationResponse.Content.ReadAsStringAsync();
				var errorDto = JsonSerializer.Deserialize<ErrorDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
				return Response<bool>.Fail(errorDto.Errors, StatusCodes.Status400BadRequest);
			}

			// User creation successful
			return Response<bool>.Success(StatusCodes.Status200OK);
		}
	}


	
}


