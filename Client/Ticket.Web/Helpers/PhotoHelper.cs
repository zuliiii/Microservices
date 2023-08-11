using Microsoft.Extensions.Options;
using Ticket.Web.Models;

namespace Ticket.Web.Helpers
{
	public class PhotoHelper
	{
		private readonly ServiceApiSettings _serviceApiSettings;

		public PhotoHelper(IOptions<ServiceApiSettings> serviceApiSettings)
		{
			_serviceApiSettings = serviceApiSettings.Value;
		}

		public string GetPhotoStockUrl(string photoUrl)
		{
			return $"{_serviceApiSettings.PhotoStockUri}/photos/{photoUrl}";
		}
	}
}
