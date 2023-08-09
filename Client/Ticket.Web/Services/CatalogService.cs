using Ticket.Web.Models.Catalog;
using Ticket.Web.Services.Interfaces;

namespace Ticket.Web.Services
{
	public class CatalogService : ICatalogService
	{
		private readonly HttpClient _httpClient;

		public CatalogService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public Task<bool> CreateEventAsync(EventCreateInput eventCreateInput)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteEventAsync(string eventId)
		{
			throw new NotImplementedException();
		}

		public Task<List<CategoryViewModel>> GetAllCategoryAsync()
		{
			throw new NotImplementedException();
		}

		public Task<List<EventViewModel>> GetAllEventAsync()
		{
			throw new NotImplementedException();
		}

		public Task<EventViewModel> GetByEventId(string eventId)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateEventAsync(EventCreateInput eventUpdateInput)
		{
			throw new NotImplementedException();
		}
	}
}
